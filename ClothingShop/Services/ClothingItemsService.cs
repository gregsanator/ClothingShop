using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClothingShop.DTO.ClothingItems;

namespace ClothingShop.Services
{
    public class ClothingItemsService
    {
        public List<ClothingItemsListItem> List(Guid? subcategoryId)
        //if subcategoryId != null than list all avalable products with this categoryId else list all avalable products
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItems> item = context.ClothingItems;
                if (subcategoryId != null)
                    item = item.Where(a => a.SubcategoryId == subcategoryId);

                List<ClothingItemsListItem> list = item.Select(a => new ClothingItemsListItem
                {
                    Id = a.Id,
                    Name = a.Name,
                    Availability = context.ClothingItemsSizes.Any(b => b.ClothingItemId == a.Id && b.Quantity > 0),
                    Price = a.Price
                }).ToList();

                return list;
            }
        }

        public ClothingItemsForm Details(Guid itemId) // details 
        {
            using (var context = new ClothingShopDbContext())
            {
                ClothingItemsForm item = context.ClothingItems.Where(a => a.Id == itemId).Select(a => new ClothingItemsForm
                {
                    Name = a.Name,
                    BrandName = a.Brand.Name,
                    Availability = context.ClothingItemsSizes.Any(b => b.ClothingItemId == itemId && b.Quantity > 0),
                    Description = a.Description,
                    Gender = a.Gender,
                    Price = a.Price
                }).FirstOrDefault();

                return item;
            }
        }

        public bool Save(ClothingItemsSave model)
        {
            using (var context = new ClothingShopDbContext())
            {
                ClothingItems item = new ClothingItems
                {
                    Id = model.Id,
                    Name = model.Name,
                    BrandId = model.BrandId,
                    Description = model.Description,
                    Gender = model.Gender,
                    SubcategoryId = model.SubcategoryId,
                    Price = model.Price,
                    SizeType = model.SizeType
                };

                if (item.Id != Guid.Empty)
                {
                    context.ClothingItems.Attach(item);
                    context.Entry(item).Property(a => a.Name).IsModified = true;
                    context.Entry(item).Property(a => a.SubcategoryId).IsModified = true;
                    context.Entry(item).Property(a => a.Gender).IsModified = true;
                    context.Entry(item).Property(a => a.Description).IsModified = true;
                    context.Entry(item).Property(a => a.BrandId).IsModified = true;
                }
                else
                    context.ClothingItems.Add(item);
                context.SaveChanges();

                return true;
            }
        }

        public List<ClothingItemsListItem> ListItems(Guid id) // filter and show all the products 
        {
            using (var context = new ClothingShopDbContext())
            {
                List<ClothingItemsListItem> list = context.ClothingItemsSizes.Select(a => new ClothingItemsListItem
                {
                    Id = a.ClothingItem.Id,
                    Name = a.ClothingItem.Name,
                    Price = a.ClothingItem.Price,
                    Availability = a.Quantity > 0,
                    BrandId = a.ClothingItem.BrandId,
                    SizeId = a.SizeId,
                    SubcategoryId = a.ClothingItem.SubcategoryId
                }).ToList();

                if (context.UserSubcategoriesFilter.Where(a => a.UserId == id).Any())
                {
                    list = (from l in list
                            join sub in context.UserSubcategoriesFilter.Where(a => a.UserId == id)
                            on l.SubcategoryId equals sub.SubcategoryId
                            select new ClothingItemsListItem
                            {
                                Id = l.Id,
                                Name = l.Name,
                                Price = l.Price,
                                Availability = l.Price > 0,
                                BrandId = l.BrandId,
                                SizeId = l.SizeId,
                                SubcategoryId = l.SubcategoryId
                            }).ToList();
                    list.Select(a => a.Id).Distinct();
                }

                if (context.UserSizesFilter.Where(a => a.UserId == id).Any())
                {
                    list = (from l in list
                            join us in context.UserSizesFilter.Where(a => a.UserId == id)
                            on l.SizeId equals us.SizeId
                            select new ClothingItemsListItem
                            {
                                Id = l.Id,
                                Name = l.Name,
                                Price = l.Price,
                                Availability = l.Price > 0,
                                BrandId = l.BrandId,
                                SizeId = l.SizeId,
                                SubcategoryId = l.SubcategoryId
                            }).ToList();
                    list.Select(a => a.Id).Distinct();
                }

                if (context.UserBrandsFilter.Where(a => a.UserId == id).Any())
                {
                    list = (from l in list
                            join ub in context.UserBrandsFilter.Where(a => a.UserId == id)
                            on l.BrandId equals ub.BrandId
                            select new ClothingItemsListItem
                            {
                                Id = l.Id,
                                Name = l.Name,
                                Price = l.Price,
                                Availability = l.Price > 0,
                                BrandId = l.BrandId,
                                SizeId = l.SizeId,
                                SubcategoryId = l.SubcategoryId
                            }).ToList();
                    list.Select(a => a.Id).Distinct();
                }
                return list;
            }
        }
    }
}