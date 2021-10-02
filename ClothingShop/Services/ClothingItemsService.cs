﻿using ClothingShop.Models;
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
                    BrandName = a.BrandName,
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
                    BrandName = model.BrandName,
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
                    context.Entry(item).Property(a => a.BrandName).IsModified = true;
                }
                else
                    context.ClothingItems.Add(item);
                context.SaveChanges();

                return true;
            }
        }
    }
}