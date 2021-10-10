using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.Services
{
    public class ShopsService
    {
        public List<ShopsListItem> List() //list the availabillity of a certain product with a certain size
        {
            using (var context = new ClothingShopDbContext())
            {
                List<ShopsListItem> list = context.Shops.Select(a => new ShopsListItem
                {
                    Id = a.Id,
                    Location = a.Location,
                    Name = a.Name,
                    WorkingHours = a.WorkingHours
                }).ToList();
                return list;
            }
        }

        public bool Save(ShopsSave model)
        // Add the quantity of stock of a certain product with a certain size
        {
            using (var context = new ClothingShopDbContext())
            {
                Shops shop = new Shops
                {
                    Id = model.Id,
                    Location = model.Location,
                    Name = model.Name,
                    WorkingHours = model.WorkingHours
                };

                if (shop.Id != Guid.Empty)
                {
                    context.Shops.Attach(shop);
                    context.Entry(shop).Property(a => a.Location).IsModified = true;
                    context.Entry(shop).Property(a => a.Name).IsModified = true;
                    context.Entry(shop).Property(a => a.WorkingHours).IsModified = true;
                }

                else
                    context.Shops.Attach(shop);
                context.SaveChanges();
                return true;
            }
        }
    }
}