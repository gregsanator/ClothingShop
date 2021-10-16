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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClubCards>()
                .HasRequired(c => c.User)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public DbSet<Administrators> Administrators { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<UserSizesFilter> UserSizesFilter { get; set; }
        public DbSet<UserBrandFilter> UserBrandsFilter { get; set; }
        public DbSet<UserSubcategoriesFilter> UserSubcategoriesFilter { get; set; }
        public DbSet<AdministratorsPermissions> AdministratorsPermissions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<ClothingItems> ClothingItems { get; set; }
        public DbSet<ClothingItemsSizes> ClothingItemsSizes { get; set; }
        public DbSet<ClothingItemsPromotions> ClothingItemsPromotions { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<ClubCards> ClubCards { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Promotions> Promotions { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Subcategories> Subcategories { get; set; }
        public DbSet<Carts> Carts { get; set; }
    }
}