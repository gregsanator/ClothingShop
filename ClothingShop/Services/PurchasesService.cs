using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.Services
{
    public class PurchasesService
    {
        public List<PurchasesListItem> List(Guid? id) // returns a list of all purchases made by the user or all the purchases made on the shop(if admin)
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<Purchases> purchases = context.Purchases;
                if (id != null)
                    purchases = purchases.Where(a => a.UserId == id);

                List<PurchasesListItem> list = purchases.Select(a => new PurchasesListItem
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    ClothingItemSizeId = a.ClothingItemSizeId,
                    Quantity = a.Quantity,
                    TotalPrice = a.TotalPrice,
                    DateOrdered = a.DateOrdered,
                    DiscountPercantage = a.DiscountPercantage
                }).ToList();
                return list;
            }
        }
    }
}