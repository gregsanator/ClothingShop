using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClothingShop.DTO.Categories;

namespace ClothingShop.Services
{
    public class CategoriesService
    {
        public List<CategoriesListItem> List()
        {
            using (var context = new ClothingShopDbContext())
            {
                List<CategoriesListItem> list = context.Categories.Select(a => new CategoriesListItem
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();
                return list;
            }
        }

        public bool Save(CategoriesSave model)
        {
            using (var context = new ClothingShopDbContext())
            {
                Categories category = new Categories
                {
                    Id = model.Id,
                    Name = model.Name
                };

                if (category.Id != Guid.Empty)
                {
                    context.Categories.Attach(category);
                    context.Entry(category).Property(a => a.Name).IsModified = true;
                }
                else
                    context.Categories.Add(category);
                context.SaveChanges();

                return true;
            }
        }
    }
}