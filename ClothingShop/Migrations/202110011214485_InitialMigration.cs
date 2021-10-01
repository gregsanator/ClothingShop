namespace ClothingShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EMail = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClothingItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                        Availability = c.Boolean(nullable: false),
                        Gender = c.Int(nullable: false),
                        BrandName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subcategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ClubCards",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        AdministratorId = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Points = c.Double(nullable: false),
                        DiscountPercantage = c.Double(),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Administrators", t => t.AdministratorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AdministratorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EMail = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ClothingItemId = c.Guid(nullable: false),
                        DiscountPercantage = c.Double(nullable: false),
                        DiscountName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClothingItems", t => t.ClothingItemId, cascadeDelete: true)
                .Index(t => t.ClothingItemId);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        WorkingHours = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SizeTypeId = c.Guid(nullable: false),
                        Size = c.Int(nullable: false),
                        StockNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SizeTypes", t => t.SizeTypeId, cascadeDelete: true)
                .Index(t => t.SizeTypeId);
            
            CreateTable(
                "dbo.SizeTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ClothingItemId = c.Guid(nullable: false),
                        Size = c.Int(nullable: false),
                        SizeDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClothingItems", t => t.ClothingItemId, cascadeDelete: true)
                .Index(t => t.ClothingItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sizes", "SizeTypeId", "dbo.SizeTypes");
            DropForeignKey("dbo.SizeTypes", "ClothingItemId", "dbo.ClothingItems");
            DropForeignKey("dbo.Promotions", "ClothingItemId", "dbo.ClothingItems");
            DropForeignKey("dbo.ClubCards", "UserId", "dbo.Users");
            DropForeignKey("dbo.ClubCards", "AdministratorId", "dbo.Administrators");
            DropForeignKey("dbo.ClothingItems", "CategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.Subcategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.SizeTypes", new[] { "ClothingItemId" });
            DropIndex("dbo.Sizes", new[] { "SizeTypeId" });
            DropIndex("dbo.Promotions", new[] { "ClothingItemId" });
            DropIndex("dbo.ClubCards", new[] { "AdministratorId" });
            DropIndex("dbo.ClubCards", new[] { "UserId" });
            DropIndex("dbo.Subcategories", new[] { "CategoryId" });
            DropIndex("dbo.ClothingItems", new[] { "CategoryId" });
            DropTable("dbo.SizeTypes");
            DropTable("dbo.Sizes");
            DropTable("dbo.Shops");
            DropTable("dbo.Promotions");
            DropTable("dbo.Permissions");
            DropTable("dbo.Users");
            DropTable("dbo.ClubCards");
            DropTable("dbo.Subcategories");
            DropTable("dbo.ClothingItems");
            DropTable("dbo.Categories");
            DropTable("dbo.Administrators");
        }
    }
}
