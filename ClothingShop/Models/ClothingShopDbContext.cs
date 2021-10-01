using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class ClothingShopDbContext : DbContext
    {
        public ClothingShopDbContext() : base("name=ClothingShop")
        {

        }

        public DbSet<Administrators> Administrators { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<ClothingItems> ClothingItems { get; set; }
        public DbSet<ClubCards> ClubCards { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Promotions> Promotions { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<SizeTypes> SizeTypes { get; set; }
        public DbSet<Subcategories> Subcategories { get; set; }
    }
}