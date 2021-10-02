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
        public List<CartsListItem> ListItems(Guid id) // list all the items that are inside the cart for a given user 
        {
            using(var context = new ClothingShopDbContext())
            {
                List<CartsListItem> list = context.Carts.Where(a => a.UserId == id).Select(a => new CartsListItem
                {
                    Id = a.Id,
                    Name = a.User.Name,
                    BrandName = a.ClothingItemSize.ClothingItem.Name,
                    Size = a.ClothingItemSize.Size.Size,
                    Quantity = a.Quantity,
                    Price = a.ClothingItemSize.ClothingItem.Price
                }).ToList();

                return list;
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

        public double TotalPrice(Guid id) //get all the cart items and calculate price if the user has a club card than add discount to final price 
        {
            using (var context = new ClothingShopDbContext())
            {
                List<CartsPrice> itemPrice = context.Carts.Where(a => a.UserId == id).Select(a => new CartsPrice
                {
                    Price = a.Quantity * a.ClothingItemSize.ClothingItem.Price
                }).ToList();

                double totalPrice = itemPrice.Sum(a => Convert.ToInt32(a));

                if (context.ClubCards.Any(a => a.UserId == id))
                {
                    double clubCardDiscount = context.ClubCards.Where(a => a.UserId == id).Select(a => a.DiscountPercantage).FirstOrDefault();
                    totalPrice *= (1 - (clubCardDiscount / 100));
                }
                return totalPrice;
            }
        }

        public bool CartsCheckOut(Guid id) // delete all the carts after the user has confirmed the order and change the stock number
        {
            using (var context = new ClothingShopDbContext())
            {
                List<CartsOrder> carts = context.Carts.Where(a => a.UserId == id).Select(a => new CartsOrder
                {
                    CartId = a.Id,
                    ClothingItemSizeId = a.ClothingItemSize.Id,
                    Quantity = a.Quantity,
                    UserId = a.UserId,
                    DateOrdered = DateTime.Now,
                    TotalPrice = a.Quantity * a.ClothingItemSize.ClothingItem.Price,
                    DiscountPercantage = a.User.ClubCards.DiscountPercantage
                }).ToList();

                foreach (var item in carts)
                {
                    Carts cart = context.Carts.Where(a => a.Id == item.CartId).FirstOrDefault(); // for deleting a cart

                    Purchases purchase = new Purchases // for making a new purchase(which similar prop to cart)
                    {
                        UserId = item.UserId,
                        ClothingItemSizeId = item.ClothingItemSizeId,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                        DateOrdered = item.DateOrdered
                    };

                    if (context.ClubCards.Where(a => a.UserId == id).Any()) // for adjusting profits if user had discounts
                    {
                        purchase.DiscountPercantage = item.DiscountPercantage;
                        purchase.TotalPrice *= (1 - (item.DiscountPercantage / 100));
                    }

                    ClubCards clubCard = context.ClubCards.Where(a => a.UserId == id).FirstOrDefault();
                    clubCard.Points += purchase.TotalPrice / 10; // adjusting the points of the user's clubcard(adding total price / 100 to the total points)

                    Models.ClothingItemsSizes itemSize = context.ClothingItemsSizes.Where(a => a.Id == item.ClothingItemSizeId).FirstOrDefault(); 
                    itemSize.Quantity -= item.Quantity; // adjusting quantity of product with certain size

                    context.Carts.Remove(cart);
                    context.Purchases.Add(purchase);
                }
                context.SaveChanges();
                return true;   
            }
        }
    }
}