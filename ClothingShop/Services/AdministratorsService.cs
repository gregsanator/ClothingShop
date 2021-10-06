using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClothingShop.DTO.Administrators;

namespace ClothingShop.Services
{
    public class AdministratorsService
    {
        public List<AdministratorsListItem> List()
        {
            using (var context = new ClothingShopDbContext())
            {
                return context.Administrators.Select(a => new AdministratorsListItem
                {
                    Id = a.Id,
                    EMail = a.EMail,
                    Enabled = a.Enabled
                }).ToList();
            }
        }

        public AdministratorsForm Details(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                return context.Administrators.Where(a => a.Id == id).Select(a => new AdministratorsForm
                {
                    Name = a.Name,
                    Surname = a.Surname,
                    EMail = a.EMail,
                    Enabled = a.Enabled
                }).FirstOrDefault();
            }
        }

        public bool Save(AdministratorsSave save)
        {
            using (var context = new ClothingShopDbContext())
            {
                Administrators admin = new Administrators
                {
                    Id = save.Id,
                    EMail= save.EMail,
                    Password = save.Password,
                    Name = save.Name,
                    Surname = save.Surname,
                };
                if (admin.Id != Guid.Empty)
                {
                    context.Administrators.Attach(admin);
                    context.Entry(admin).Property(a => a.EMail).IsModified = true;
                    context.Entry(admin).Property(a => a.Password).IsModified = true;
                    context.Entry(admin).Property(a => a.Name).IsModified = true;
                    context.Entry(admin).Property(a => a.Surname).IsModified = true;
                }
                else
                    context.Administrators.Add(admin);
                context.SaveChanges();
                return true;
            }
        }

        public bool Enabled(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                Administrators admin = context.Administrators.Where(a => a.Id == id).FirstOrDefault();
                if (admin == null)
                    return false;
                admin.Enabled = !admin.Enabled;
                context.SaveChanges();
                return true;
            }
        }

        public List<AdministratorPermissions> Permissions(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                List<AdministratorPermissions> joined = (from p in context.Permissions
                                                          join ap in context.AdministratorsPermissions.Where(a => a.AdministratorId == id)
                                                          on p.Id equals ap.PermissionId into joinedpap
                                                          from pap in joinedpap.DefaultIfEmpty()
                                                          select new AdministratorPermissions
                                                          {
                                                              Id = p.Id,
                                                              Name = p.Name,
                                                              Enabled = pap != null
                                                          }).ToList();
                return joined;
            }
        }

        public bool AdministratorsPermissionEnabled(AdministratorsPermissionEnabled model)
        {
            using (var context = new ClothingShopDbContext())
            {
                AdministratorsPermissions administratorPermission = context.AdministratorsPermissions.Where(a => a.PermissionId == model.PermissionId &&
                a.AdministratorId == model.AdministratorId).FirstOrDefault();

                if (administratorPermission == null)
                {
                    AdministratorsPermissions ap = new AdministratorsPermissions
                    {
                        AdministratorId = model.AdministratorId,
                        PermissionId = model.PermissionId
                    };
                    context.AdministratorsPermissions.Add(ap);
                }

                else
                    context.AdministratorsPermissions.Remove(administratorPermission);
                context.SaveChanges();
                return true;
            }
        }
    }
}