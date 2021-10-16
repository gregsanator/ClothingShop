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
        public List<ClothingItemsListItem> FilterItems(FilterItems filter) // checkboc filters - if none checked return normal list
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> items = context.ClothingItemsSizes.Where(a => a.Quantity > 0);

                if (filter.Brands.Any() || filter.Subcategories.Any() || filter.Sizes.Any())
                { 
                    items.Where(a => (filter.Subcategories.Count == 0 || filter.Subcategories.Contains(a.ClothingItem.SubcategoryId)) &&
                                        (filter.Sizes.Count == 0 || filter.Sizes.Contains(a.SizeId)) &&
                                    (filter.Brands.Count == 0 || filter.Brands.Contains(a.ClothingItem.BrandId)));
                }
                return List(items);
            }
        }


        public List<ClothingItemsListItem> Newest() // newest items
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> items = context.ClothingItemsSizes.Where(a => a.ClothingItem.DateAdded >= DateTime.Now.AddDays(-30));
                return List(items);
            }
        }


        public List<ClothingItemsListItem> FilterPrice(PriceFilters filter, List<ClothingItemsListItem> list)
        // Price filter (on searched items/checkbox filters/normal list)
        {
            list = list.Where(a => ((!filter.HighestPrice.HasValue || a.Price < filter.HighestPrice)) &&
            (!filter.LowestPrice.HasValue || a.Price > filter.LowestPrice)).ToList();

            return list;
        }


        public List<ClothingItemsListItem> Search(string filter, List<ClothingItemsListItem> list) // search items(normal list and checkbox listed)
        {
            if (!string.IsNullOrEmpty(filter))
                list = list.Where(a => a.Name.Contains(filter) || a.BrandName.Contains(filter)).ToList();

            return list;
        }


        public List<ClothingItemsListItem> Sort(int sortType, List<ClothingItemsListItem> list) // order lists(normal list item/searched/checkbox list) 
        {
            if (sortType == 0)// Price filter Lowest -> Highest
                list.OrderBy(a => a.Price);
            else if (sortType == 1) // Highest -> Lowest
                list.OrderByDescending(a => a.Price);
            else if (sortType == 2) // Newest
                list.OrderByDescending(a => a.DateAdded);

            return list;
        }

        public List<ClothingItemsListItem> List(IQueryable<ClothingItemsSizes> items) // List items
        {
            using (var context = new ClothingShopDbContext())
            {
                items = items.GroupBy(a => a.ClothingItemId).Select(a => a.First()); // eliminating all duplicate records

                List<ClothingItemsListItem> list = items.Select(a => new ClothingItemsListItem
                {
                    Id = a.ClothingItem.Id,
                    Name = a.ClothingItem.Name,
                    Price = a.ClothingItem.Price,
                    BrandName = a.ClothingItem.Brand.Name,
                    DateAdded = a.ClothingItem.DateAdded
                }).ToList();

                return list;
            }
        }


        public ClothingItemsForm Details(Guid itemId) // details 
        {
            using (var context = new ClothingShopDbContext())
            {
                ClothingItemsForm item = context.ClothingItemsSizes.Where(a => a.Id == itemId).Select(a => new ClothingItemsForm
                {
                    Name = a.ClothingItem.Name,
                    BrandName = a.ClothingItem.Brand.Name,
                    Description = a.ClothingItem.Description,
                    Gender = a.ClothingItem.Gender,
                    Price = a.ClothingItem.Price
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
                    SizeType = model.SizeType,
                    DateAdded = DateTime.Now
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