using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.Services
{
    public class CartsService
    {
        public CartsListItem List(Guid id) // list all the items that are inside the cart for a given user 
        {
            using (var context = new ClothingShopDbContext())
            {
                CartsListItem item = new CartsListItem();
                item.CartList = context.Carts.Where(a => a.UserId == id).Select(a => new CartsList
                {
                    Id = a.Id,
                    Name = a.User.Name,
                    BrandName = a.ClothingItemSize.ClothingItem.Name,
                    Size = a.ClothingItemSize.Size.Size,
                    Quantity = a.Quantity,
                    Price = a.ClothingItemSize.ClothingItem.Price * a.Quantity,
                    PriceAfterDiscount = a.ClothingItemSize.ClothingItem.Price * a.Quantity * (1 - (a.User.ClubCards.DiscountPercantage / 100))
                }).ToList();

                item.TotalPrice = item.CartList.Sum(a => a.PriceAfterDiscount);

                return item;
            }
        }

        public bool AddItemToCart(CartsSave model) //when the custumer adds an item to the cart
        {
            using (var context = new ClothingShopDbContext())
            {
                Carts cart = new Carts
                {
                    Id = model.Id,
                    ClothingItemSizeId = model.ClothingItemSizeId,
                    UserId = model.UserId,
                    Quantity = model.Quantity
                };

                if (cart.Id != Guid.Empty)
                {
                    context.Carts.Attach(cart);
                    context.Entry(cart).Property(a => a.ClothingItemSizeId).IsModified = true;
                    context.Entry(cart).Property(a => a.Quantity).IsModified = true;
                }

                else
                    context.Carts.Add(cart);
                context.SaveChanges();

                return true;
            }
        }

        public bool DeleteItemFromCart(Guid id) //deleting an item from the cart
        {
            using (var context = new ClothingShopDbContext())
            {
                Carts cartItem = context.Carts.Where(a => a.Id == id).FirstOrDefault();
                if (cartItem != null)
                    return false;
                else
                    context.Carts.Remove(cartItem);
                return true;
            }
        }

        public bool PurchaseItems(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                foreach (var item in context.Carts.Where(a => a.UserId == id))
                {
                    Purchases purchase = new Purchases
                    {
                        UserId = item.UserId,
                        ClothingItemSizeId = item.ClothingItemSizeId,
                        Quantity = item.Quantity,
                        TotalPrice = item.ClothingItemSize.ClothingItem.Price * item.Quantity * (1 - (item.User.ClubCards.DiscountPercantage / 100)),
                        DateOrdered = DateTime.Now,
                        DiscountPercantage = item.User.ClubCards.DiscountPercantage,
                        PromotionId = context.ClothingItemsPromotions.Where
                            (a => a.ClothingItemId == item.ClothingItemSize.ClothingItem.Id).FirstOrDefault().ClothingItem.Id
                    };

                    if (context.Users.Where(a => a.Id == id).Any())
                    {
                        ClubCards clubCard = context.ClubCards.Where(a => a.UserId == id).FirstOrDefault();
                        clubCard.Points += purchase.TotalPrice / 10; // adjusting the points of the user's clubcard(adding total price / 100 to the total points)
                    }

                    Models.ClothingItemsSizes itemSize = context.ClothingItemsSizes.Where(a => a.Id == item.ClothingItemSizeId).FirstOrDefault();
                    itemSize.Quantity -= item.Quantity; // adjusting quantity of product with certain size

                    context.Carts.Remove(item);
                    context.Purchases.Add(purchase);
                }
                context.SaveChanges();
                return true;
            }
        }
    }
}