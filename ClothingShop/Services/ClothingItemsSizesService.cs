using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClothingShop.DTO.ClothingItemsSizes;

namespace ClothingShop.Services
{
    public class ClothingItemsSizesService
    {
        //This service is for the head administrators to see the total stock number of a ClothingItemSIze
        public List<ClothingItemsSizesListItem> List(string brandName) //list the availabillity of a certain product with a certain size
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> itemsSizes = context.ClothingItemsSizes;
                if (brandName != null)
                    itemsSizes = itemsSizes.Where(a => a.ClothingItem.Brand.Name == brandName);

                List<ClothingItemsSizesListItem> list = (from i in itemsSizes
                                                         group i by i.ClothingItemId into cisGroup
                                                         select new ClothingItemsSizesListItem
                                                         {
                                                             ClothingItemId = cisGroup.Key,
                                                             Name = cisGroup.Select(a => a.ClothingItem.Name).FirstOrDefault(),
                                                             Quantity = cisGroup.Sum(a => a.Quantity),
                                                             SizesInStock = cisGroup.Select(a => a.Size.Size).ToList(),
                                                             LocationsInStock = cisGroup.Select(a => a.Shop.Location).ToList(),
                                                             LocationsOutOfStock = context.Shops.Select(a => a.Name).
                                                                Except(cisGroup.Select(a => a.Shop.Location)).ToList(),
                                                             SizesNotInStock = context.Sizes.Where
                                                                (a => a.SizeType == cisGroup.Select(b => b.Size.SizeType).FirstOrDefault()).Select(a => a.Size).
                                                                Except(cisGroup.Select(a => a.Size.Size)).ToList()
                                                         }).ToList();
                return list;
            }
        }

        public bool Save(ClothingItemSizesSave model)
        // Add the quantity of stock of a certain product with a certain size
        {
            using (var context = new ClothingShopDbContext())
            {
                ClothingItemsSizes ciss = new ClothingItemsSizes
                {
                    Id = model.Id,
                    ClothingItemId = model.ClothingItemId,
                    SizeId = model.SizeId,
                    Quantity = model.Quantity,
                    ShopId = model.ShopId
                };

                if (ciss.Id != Guid.Empty)
                {
                    context.ClothingItemsSizes.Attach(ciss);
                    context.Entry(ciss).Property(a => a.Quantity).IsModified = true;
                    context.Entry(ciss).Property(a => a.SizeId).IsModified = true;
                    context.Entry(ciss).Property(a => a.ClothingItemId).IsModified = true;
                    context.Entry(ciss).Property(a => a.ShopId).IsModified = true;
                }

                else
                    context.ClothingItemsSizes.Attach(ciss);
                context.SaveChanges();
                return true;
            }
        }
    }
}