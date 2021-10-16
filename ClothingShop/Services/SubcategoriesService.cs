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
            using (var context = new ClothingShopDbContext()) // group join so that we can see the total number of items that is in the subcategory
            {
                List<SubcategoriesListItem> list = (from s in context.Subcategories.Where(a => a.CategoryId == id)
                                                    join c in context.ClothingItems
                                                    on s.Id equals c.SubcategoryId into Groupedsc
                                                    select new SubcategoriesListItem
                                                    {
                                                        Id = s.Id,
                                                        Name = s.Name,
                                                        NumberOfItems = Groupedsc.Count()
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

        /*public List<DTO.UserSubcategoriesFilter> SubcategoriesFilter(Guid id)
        // join Subcategories table with UserSubcategories table to see which Subcategories user ticked
        {
            using (var context = new ClothingShopDbContext())
            {
                List<DTO.UserSubcategoriesFilter> brandFilter = (from b in context.Subcategories
                                                      join us in context.UserSubcategoriesFilter.Where(a => a.UserId == id)
                                                      on b.Id equals us.SubcategoryId
                                                      select new DTO.UserSubcategoriesFilter
                                                      {
                                                          Id = b.Id,
                                                          Name = b.Name,
                                                          Enabled = us != null
                                                      }).ToList();

                return brandFilter;
            }
        }

        public bool UserSubcategoriesEnable(UserSubcategoriesEnable model) // adds or removes a userSubcategory to/from table
        {
            using (var context = new ClothingShopDbContext())
            {
                Models.UserSubcategoriesFilter filter = context.UserSubcategoriesFilter.Where
                    (a => a.SubcategoryId == model.SubcategoryId && a.UserId == model.UserId).FirstOrDefault();
                if (filter != null)
                    context.UserSubcategoriesFilter.Remove(filter);
                else
                {
                    filter = new Models.UserSubcategoriesFilter
                    {
                        UserId = model.UserId,
                        SubcategoryId = model.SubcategoryId
                    };
                    context.UserSubcategoriesFilter.Add(filter);
                }
                context.SaveChanges();
                return true;
            }
        }*/
    }
}