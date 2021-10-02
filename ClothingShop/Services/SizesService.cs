using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClothingShop.DTO.ClothingItems;
using static ClothingShop.DTO.Sizes;

namespace ClothingShop.Services
{
    public class SizesService
    {
        public List<SizesListItem> List() // list all the items that are inside the cart for a given user 
        {
            using (var context = new ClothingShopDbContext())
            {
                List<SizesListItem> list = context.Sizes.Select(a => new SizesListItem
                {
                    Id = a.Id,
                    SizeType = a.SizeType,
                    Size =a.Size
                }).ToList();

                return list;
            }
        }

        public bool Save(SizesSave model) // list all the items that are inside the cart for a given user 
        {
            using (var context = new ClothingShopDbContext())
            {
                Sizes size = new Sizes
                {
                    Id = model.Id,
                    Size = model.Size,
                    SizeType = model.SizeType
                };

                if (size.Id != Guid.Empty)
                {
                    context.Sizes.Attach(size);
                    context.Entry(size).Property(a => a.SizeType).IsModified = true;
                    context.Entry(size).Property(a => a.Size).IsModified = true;
                }
                else
                    context.Sizes.Add(size);
                context.SaveChanges();
                return true;
            }
        }


        public List<ClothingItemSizes> Sizes(Guid id)
        // Load all the sizes for a certain type - if item is shoe than load shoe sizes if wardrobe wardrobe sizes etc. Load avalable sizes in stock
        {
            using (var context = new ClothingShopDbContext())
            {
                int sizeType = context.ClothingItems.Where(a => a.Id == id).Select(a => a.SizeType).FirstOrDefault();

                List<ClothingItemSizes> sizes = (from s in context.Sizes.Where(a => a.SizeType == sizeType)
                                                 join cis in context.ClothingItemsSizes.Where(a => a.ClothingItemId == id)
                                                 on s.Id equals cis.SizeId into joinedscis
                                                 from scis in joinedscis.DefaultIfEmpty()
                                                 select new ClothingItemSizes
                                                 {
                                                     SizeId = s.Id,
                                                     Size = s.Size,
                                                     Enabled = scis.Quantity != 0

                                                 }).ToList();
                return sizes;
            }
        }
    }
}