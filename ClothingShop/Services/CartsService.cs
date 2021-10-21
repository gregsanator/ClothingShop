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
                IQueryable<Carts> userCarts = context.Carts.Where(a => a.UserId == id);
                ClubCards clubCard = context.ClubCards.Where(a => a.UserId == id).FirstOrDefault();

                double discountPercent = clubCard != null ? clubCard.DiscountPercantage : 0;

                CartsListItem item = new CartsListItem();
                item.CartList = userCarts.Select(a => new CartsList
                {
                    Id = a.Id,
                    BrandName = a.ClothingItemSize.ClothingItem.Name,
                    Size = a.ClothingItemSize.Size.Size,
                    Quantity = a.Quantity,
                    Price = item.TotalPrice == 0 ? a.ClothingItemSize.ClothingItem.Price * a.Quantity :
                    a.ClothingItemSize.ClothingItem.Price * a.Quantity * (1 - (item.TotalPrice / 100))
                }).ToList();

                item.TotalPrice = item.CartList.Sum(a => a.Price);


                return item;
            }
        }

        public void AddPoints(ClubCards clubCard, double points) // list all the items that are inside the cart for a given user 
        {
            using (var context = new ClothingShopDbContext())
            {
                clubCard.Points += points;
                context.SaveChanges();
            }
        }

        public void SavePurchases(List<PurchaseSave> list) // list all the items that are inside the cart for a given user 
        {
            using (var context = new ClothingShopDbContext())
            {
                foreach (var item in list)
                {
                    Purchases purchase = new Purchases
                    {
                        ClothingItemSizeId = item.ClothingItemSizeId,
                        UserId = item.UserId,
                        DiscountPercantage = item.DiscountPercantage,
                        DateOrdered = item.DateOrdered,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    };
                    context.Purchases.Add(purchase);
                }
                context.SaveChanges();
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

        public bool PurchaseItems(PurchaseCarts model)
        {
            using (var context = new ClothingShopDbContext())
            {

                ClubCards clubCard = context.ClubCards.Where(a => a.UserId == model.Id).FirstOrDefault();
                IQueryable<Carts> userCarts = context.Carts.Where(a => a.UserId == model.Id);

                double discountPercent = clubCard != null ? clubCard.DiscountPercantage : 0; // if user has clubcard than discount% = clubcardDiscount% else it is 0

                List<PurchaseSave> purchases = userCarts.Select(a => new PurchaseSave // map into PurchaseSave DTO
                {
                    UserId = a.UserId,
                    ClothingItemSizeId = a.ClothingItemSizeId,
                    Quantity = a.Quantity,
                    TotalPrice = discountPercent == 0 ? a.Quantity * a.ClothingItemSize.ClothingItem.Price : 
                        a.Quantity * a.ClothingItemSize.ClothingItem.Price * (1 - (discountPercent / 100)), // if discount 0 than original price if not 0 calculate discounted price
                    DateOrdered = DateTime.Now,
                    DiscountPercantage = discountPercent
                }).ToList();

                context.Carts.RemoveRange(userCarts); // remove all the carts from the carts table

                if(clubCard != null) // if the user has a clubcards than add points
                {
                    double bonusPoints = purchases.Sum(a => a.TotalPrice) / 10;
                    AddPoints(clubCard, bonusPoints);
                }

                SavePurchases(purchases); //save purchases

                context.SaveChanges();
                
                return true;
            }
        }
    }
}