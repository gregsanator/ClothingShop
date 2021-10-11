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
        public List<ClothingItemsListItem> List(FilterItems filter) // filter and show all the products 
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> items = context.ClothingItemsSizes;
                if (filter.Search.Length != 0)
                    items = items.Where(a => a.ClothingItem.Name.Contains(filter.Search) || a.ClothingItem.Brand.Name.Contains(filter.Search));

                else
                {
                    items = items.Where(a => (!filter.Subcategories.Any() || filter.Subcategories.Contains(a.ClothingItem.SubcategoryId)) && // there is no entry
                                            (!filter.Sizes.Any() || filter.Sizes.Contains(a.SizeId)) &&
                                        (!filter.Brands.Any() || filter.Brands.Contains(a.ClothingItem.BrandId)) &&
                                        a.ClothingItem.Price >= filter.LowestPrice &&
                                        a.ClothingItem.Price <= filter.HighestPrice);
                }

                //filter.SubCategories is a list of all subcategories the user selected if he has selected none than list is filled with all subcategory guids
                List<ClothingItemsListItem> list = items.Select(a => new ClothingItemsListItem
                {
                    Id = a.ClothingItem.Id,
                    Name = a.ClothingItem.Name,
                    Price = a.ClothingItem.Price,
                    Availability = a.Quantity > 0,
                    BrandName = a.ClothingItem.Brand.Name
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
    }
}