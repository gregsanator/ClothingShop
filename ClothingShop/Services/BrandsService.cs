using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClothingShop.DTO.Brands;
using UserBrandFilter = ClothingShop.Models.UserBrandFilter;

namespace ClothingShop.Services
{
    public class BrandsService
    {
        public List<BrandsListItem> List() // list all the items that are inside the cart for a given user 
        {
            using (var context = new ClothingShopDbContext())
            {
                List<BrandsListItem> list = context.Brands.Select(a => new BrandsListItem
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();

                return list;
            }
        }

        public bool Save(BrandsSave model) // list all the items that are inside the cart for a given user 
        {
            using (var context = new ClothingShopDbContext())
            {
                Brands brand = new Brands
                {
                    Id = model.Id,
                    Name = model.Name
                };

                if (brand.Id != Guid.Empty)
                {
                    context.Brands.Attach(brand);
                    context.Entry(brand).Property(a => a.Name).IsModified = true;
                }
                else
                    context.Brands.Add(brand);
                context.SaveChanges();
                return true;
            }
        }

        /*public List<UserBrandsFilter> brandFilter(Guid id)
        // join Brands table with UserBrands table to see which Brands user ticked
        {
            using (var context = new ClothingShopDbContext())
            {
                List<UserBrandsFilter> brandFilter = (from b in context.Brands
                                                      join ub in context.UserBrandsFilter.Where(a => a.UserId == id)
                                                      on b.Id equals ub.BrandId
                                                      select new UserBrandsFilter
                                                      {
                                                          Id = b.Id,
                                                          Name = b.Name,
                                                          Enabled = ub != null
                                                      }).ToList();

                return brandFilter;
            }
        }

        public bool UserBrandEnable(UserBrandsEnable model) // adds or removes a userbrand to/from table
        {
            using (var context = new ClothingShopDbContext())
            {
                UserBrandFilter filter = context.UserBrandsFilter.Where(a => a.BrandId == model.BrandId && a.UserId == model.UserId).FirstOrDefault();
                if (filter != null)
                    context.UserBrandsFilter.Remove(filter);
                else
                {
                    filter = new UserBrandFilter
                    {
                        UserId = model.UserId,
                        BrandId = model.BrandId
                    };
                    context.UserBrandsFilter.Add(filter);
                }
                context.SaveChanges();
                return true;
            }
        }*/
    }
}