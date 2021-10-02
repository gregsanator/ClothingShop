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
        public List<ClothingItemsSizesListItem> List(string brandName) //list the availabillity of a certain product with a certain size
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> itemsSizes = context.ClothingItemsSizes;
                if (brandName != null)
                    itemsSizes = itemsSizes.Where(a => a.ClothingItem.BrandName == brandName);

                List<ClothingItemsSizesListItem> list = itemsSizes.Select(a => new ClothingItemsSizesListItem
                {
                    Id = a.Id,
                    Name = a.ClothingItem.Name,
                    Quantity = a.Quantity,
                    Size = a.Size.Size
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
                    Quantity = model.Quantity
                };

                if (ciss.Id != Guid.Empty)
                {
                    context.ClothingItemsSizes.Attach(ciss);
                    context.Entry(ciss).Property(a => a.Quantity).IsModified = true;
                    context.Entry(ciss).Property(a => a.SizeId).IsModified = true;
                    context.Entry(ciss).Property(a => a.ClothingItemId).IsModified = true;
                }

                else
                    context.ClothingItemsSizes.Attach(ciss);
                context.SaveChanges();
                return true;
            }
        }
    }
}