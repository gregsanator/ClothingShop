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
        public List<ClothingItemsListItem> List(Guid id) // filter and show all the products 
        {
            using (var context = new ClothingShopDbContext())
            {
                List<ClothingItemsListItem> list = context.ClothingItemsSizes.Select(a => new ClothingItemsListItem // list of all items
                {
                    Id = a.ClothingItem.Id,
                    Name = a.ClothingItem.Name,
                    Price = a.ClothingItem.Price,
                    Availability = a.Quantity > 0,
                    BrandId = a.ClothingItem.BrandId,
                    SizeId = a.SizeId,
                    SubcategoryId = a.ClothingItem.SubcategoryId
                }).ToList();

                if (context.UserSubcategoriesFilter.Where(a => a.UserId == id).Any()) // if user selected a subcategory
                {
                    list = (from l in list // join the list from all clothesItems with a subcategory with user requested subcategory
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

                if (context.UserSizesFilter.Where(a => a.UserId == id).Any()) // if selected size
                {
                    list = (from l in list // join list with(filter/unfiltered) items and get new list
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

                if (context.UserBrandsFilter.Where(a => a.UserId == id).Any()) // same a sprevious
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
                    list.Select(a => a.Id).Distinct(); // only original Id's so that we dont have duplicate items
                }
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
    }
}