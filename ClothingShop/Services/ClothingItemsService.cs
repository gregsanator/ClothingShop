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
                IQueryable<ClothingItemsSizes> items = context.ClothingItemsSizes;
                if (filter.Brands.Any() || filter.Subcategories.Any() || filter.Sizes.Any())
                {
                    items.Where(a => (!filter.Subcategories.Any() || filter.Subcategories.Contains(a.ClothingItem.SubcategoryId)) &&
                                        (!filter.Sizes.Any() || filter.Sizes.Contains(a.SizeId)) &&
                                    (!filter.Brands.Any() || filter.Brands.Contains(a.ClothingItem.BrandId)));
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
            list.Where(a => ((!filter.HighestPrice.HasValue || a.Price < filter.HighestPrice)) &&
            (!filter.LowestPrice.HasValue || a.Price > filter.LowestPrice));
            return list;
        }


        public List<ClothingItemsListItem> Search(string filter, List<ClothingItemsListItem> list) // search items(normal list and checkbox listed)
        {
            if (!string.IsNullOrEmpty(filter))
                list.Where(a => a.Name.Contains(filter) || a.BrandName.Contains(filter));

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

        /*        public List<ClothingItemsListItem> Search(string filter) // search pr 
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> items = context.ClothingItemsSizes;
                if (!string.IsNullOrEmpty(filter))
                    items = items.Where(a => a.ClothingItem.Name.Contains(filter) || a.ClothingItem.Brand.Name.Contains(filter));

                return List(items);
            }
        }*/

        public List<ClothingItemsListItem> List(IQueryable<ClothingItemsSizes> items) // List items
        {
            using (var context = new ClothingShopDbContext())
            {
                items.Select(a => a.ClothingItemId).Distinct(); // eliminating all duplicate records

                List<ClothingItemsListItem> list = items.Select(a => new ClothingItemsListItem
                {
                    Id = a.ClothingItem.Id,
                    Name = a.ClothingItem.Name,
                    Price = a.ClothingItem.Price,
                    Availability = true,
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