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
                    Size = a.Size
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
                int sizeType = context.ClothingItems.Where(a => a.Id == id).FirstOrDefault().SizeType; // get me the sizetype of the product

                List<ClothingItemSizes> sizes = (from s in context.Sizes.Where(a => a.SizeType == sizeType) // from sizes give me all size with the sizetype above
                                                 join cis in context.ClothingItemsSizes.Where(a => a.ClothingItemId == id)//all the sizes of the clothingitem
                                                 on s.Id equals cis.SizeId into joinedscis
                                                 from scis in joinedscis.DefaultIfEmpty()// load all sizes but enabled is true if it is in stock
                                                 select new ClothingItemSizes
                                                 {
                                                     SizeId = s.Id,
                                                     Size = s.Size,
                                                     Enabled = scis.Quantity != 0

                                                 }).ToList();
                return sizes;
            }
        }

        /*public List<DTO.Sizes.UserSizesFilter> SizeFilter(Guid id)
        // join Sizes table with UserSizes table to see which sizes user ticked
        {
            using (var context = new ClothingShopDbContext())
            {
                List<DTO.Sizes.UserSizesFilter> sizeFilter = (from s in context.Sizes
                                                    join us in context.UserSizesFilter.Where(a => a.UserId == id)
                                                    on s.Id equals us.SizeId
                                                    select new DTO.Sizes.UserSizesFilter
                                                    {
                                                        Id = s.Id,
                                                        Size = s.Size,
                                                        Enabled = us != null
                                                    }).ToList();

                return sizeFilter;
            }
        }

        public bool UserSizeEnable(UserSizeEnable model) // adds or removes a userSize to/from table
        {
            using (var context = new ClothingShopDbContext())
            {
                Models.UserSizesFilter userSize = context.UserSizesFilter.Where(a => a.UserId == model.UserId && a.SizeId == model.SizeId).FirstOrDefault();
                if (userSize != null)
                    context.UserSizesFilter.Remove(userSize);
                else
                {
                    Models.UserSizesFilter us = new Models.UserSizesFilter
                    {
                        SizeId = model.SizeId,
                        UserId = model.UserId
                    };
                    context.UserSizesFilter.Add(us);
                }
                context.SaveChanges();
                return true;
            }
        }*/
    }
}