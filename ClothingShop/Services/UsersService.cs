using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.Services
{
    public class UsersService
    {
        public List<UsersListItem> List()
        {
            using (var context = new ClothingShopDbContext())
            {
                return context.Users.Select(a => new UsersListItem
                {
                    Id = a.Id,
                    Name = a.Name,
                    Surname = a.Surname,
                }).ToList();
            }
        }

        public UsersForm Details(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                return context.Users.Where(a => a.Id == id).Select(a => new UsersForm
                {
                    Name = a.Name,
                    Birthday = a.Birthday,
                    Surname = a.Surname,
                    EMail = a.EMail
                }).FirstOrDefault();
            }
        }

        public bool Save(UsersSave model)
        {
            using (var context = new ClothingShopDbContext())
            {
                Users user = new Users
                {
                    Id = model.Id,
                    Name = model.Name,
                    Birthday = model.Birthday,
                    Surname = model.Surname,
                    EMail = model.EMail,
                    Password = model.Password
                };

                if (user.Id != Guid.Empty)
                {
                    context.Users.Attach(user);
                    context.Entry(user).Property(a => a.Name).IsModified = true;
                    context.Entry(user).Property(a => a.Surname).IsModified = true;
                    context.Entry(user).Property(a => a.EMail).IsModified = true;
                    context.Entry(user).Property(a => a.Password).IsModified = true;
                    context.Entry(user).Property(a => a.Birthday).IsModified = true;
                }
                else
                {
                    context.Users.Add(user);
                }
                context.SaveChanges();
                return true;
            }
        }
    }
}