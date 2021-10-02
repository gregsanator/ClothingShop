using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.Services
{
    public class SubcategoriesService
    {
        public List<SubcategoriesListItem> List(Guid id)
        //list all subcategories for a categorie with a given id, if id is null than list all subcategories
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<Subcategories> subcategories = context.Subcategories;
                if (id != null)
                    subcategories.Where(a => a.CategoryId == id);

                List<SubcategoriesListItem> list = subcategories.Select(a => new SubcategoriesListItem
                {
                    Id = a.Id,
                    Name = a.Name,
                    NumberOfItems = context.ClothingItems.Where(b => b.SubcategoryId == a.Id).Count()
                }).ToList();
                return list;
            }
        }

        public bool Save(SubcategoriesSave model)
        {
            using (var context = new ClothingShopDbContext())
            {
                Subcategories subcategory = new Subcategories
                {
                    Id = model.Id,
                    Name = model.Name,
                    CategoryId = model.CategoryId
                };

                if (subcategory.Id != Guid.Empty)
                {
                    context.Subcategories.Attach(subcategory);
                    context.Entry(subcategory).Property(a => a.Name).IsModified = true;
                    context.Entry(subcategory).Property(a => a.CategoryId).IsModified = true;
                }
                else
                    context.Subcategories.Add(subcategory);
                context.SaveChanges();

                return true;
            }
        }
    }
}