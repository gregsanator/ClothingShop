namespace ClothingShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingChangesToModelsAndAddingNewModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Promotions", "ClothingItemId", "dbo.ClothingItems");
            DropForeignKey("dbo.SizeTypes", "ClothingItemId", "dbo.ClothingItems");
            DropForeignKey("dbo.Sizes", "SizeTypeId", "dbo.SizeTypes");
            DropForeignKey("dbo.ClubCards", "UserId", "dbo.Users");
            DropIndex("dbo.Promotions", new[] { "ClothingItemId" });
            DropIndex("dbo.Sizes", new[] { "SizeTypeId" });
            DropIndex("dbo.SizeTypes", new[] { "ClothingItemId" });
            RenameColumn(table: "dbo.ClothingItems", name: "CategoryId", newName: "SubcategoryId");
            RenameIndex(table: "dbo.ClothingItems", name: "IX_CategoryId", newName: "IX_SubcategoryId");
            DropPrimaryKey("dbo.ClubCards");
            CreateTable(
                "dbo.AdministratorsPermissions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AdministratorId = c.Guid(nullable: false),
                        PermissionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Administrators", t => t.AdministratorId, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.AdministratorId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClothingItemSizeId = c.Guid(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClothingItemsSizes", t => t.ClothingItemSizeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClothingItemSizeId);
            
            CreateTable(
                "dbo.ClothingItemsSizes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClothingItemId = c.Guid(nullable: false),
                        SizeId = c.Guid(nullable: false),
                        ShopId = c.Guid(nullable: false),
                        Quantity = c.Double(nullable: false),
                        InStock = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClothingItems", t => t.ClothingItemId, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.ClothingItemId)
                .Index(t => t.SizeId)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.ClothingItemsPromotions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClothingItemId = c.Guid(nullable: false),
                        PromotionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClothingItems", t => t.ClothingItemId, cascadeDelete: true)
                .ForeignKey("dbo.Promotions", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.ClothingItemId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ClothingItemSizeId = c.Guid(nullable: false),
                        PromotionId = c.Guid(),
                        Quantity = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        DateOrdered = c.DateTime(nullable: false),
                        DiscountPercantage = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClothingItemsSizes", t => t.ClothingItemSizeId, cascadeDelete: true)
                .ForeignKey("dbo.Promotions", t => t.PromotionId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClothingItemSizeId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.UserBrandFilters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        BrandId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.UserSizesFilters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        SizeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SizeId);
            
            CreateTable(
                "dbo.UserSubcategoriesFilters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        SubcategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subcategories", t => t.SubcategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SubcategoryId);
            
            AddColumn("dbo.Administrators", "Enabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.ClothingItems", "BrandId", c => c.Guid(nullable: false));
            AddColumn("dbo.ClothingItems", "Description", c => c.String());
            AddColumn("dbo.ClothingItems", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.ClothingItems", "SizeType", c => c.Int(nullable: false));
            AddColumn("dbo.ClothingItems", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "ClubCardId", c => c.Guid(nullable: false));
            AddColumn("dbo.Permissions", "Name", c => c.String());
            AddColumn("dbo.Promotions", "Name", c => c.String());
            AddColumn("dbo.Promotions", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promotions", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sizes", "SizeType", c => c.Int(nullable: false));
            AlterColumn("dbo.ClubCards", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ClubCards", "DiscountPercantage", c => c.Double(nullable: false));
            AlterColumn("dbo.Sizes", "Size", c => c.String());
            AddPrimaryKey("dbo.ClubCards", "Id");
            CreateIndex("dbo.ClothingItems", "BrandId");
            CreateIndex("dbo.Users", "ClubCardId");
            AddForeignKey("dbo.ClothingItems", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClubCards", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.ClothingItems", "Availability");
            DropColumn("dbo.ClothingItems", "BrandName");
            DropColumn("dbo.Permissions", "Description");
            DropColumn("dbo.Promotions", "ClothingItemId");
            DropColumn("dbo.Promotions", "DiscountName");
            DropColumn("dbo.Sizes", "SizeTypeId");
            DropColumn("dbo.Sizes", "StockNumber");
            DropTable("dbo.SizeTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SizeTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ClothingItemId = c.Guid(nullable: false),
                        Size = c.Int(nullable: false),
                        SizeDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sizes", "StockNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Sizes", "SizeTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.Promotions", "DiscountName", c => c.String());
            AddColumn("dbo.Promotions", "ClothingItemId", c => c.Guid(nullable: false));
            AddColumn("dbo.Permissions", "Description", c => c.String());
            AddColumn("dbo.ClothingItems", "BrandName", c => c.String());
            AddColumn("dbo.ClothingItems", "Availability", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.ClubCards", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSubcategoriesFilters", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSubcategoriesFilters", "SubcategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.UserSizesFilters", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSizesFilters", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.UserBrandFilters", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBrandFilters", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Purchases", "UserId", "dbo.Users");
            DropForeignKey("dbo.Purchases", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.Purchases", "ClothingItemSizeId", "dbo.ClothingItemsSizes");
            DropForeignKey("dbo.ClothingItemsPromotions", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.ClothingItemsPromotions", "ClothingItemId", "dbo.ClothingItems");
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards");
            DropForeignKey("dbo.Carts", "ClothingItemSizeId", "dbo.ClothingItemsSizes");
            DropForeignKey("dbo.ClothingItemsSizes", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.ClothingItemsSizes", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ClothingItemsSizes", "ClothingItemId", "dbo.ClothingItems");
            DropForeignKey("dbo.ClothingItems", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.AdministratorsPermissions", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.AdministratorsPermissions", "AdministratorId", "dbo.Administrators");
            DropIndex("dbo.UserSubcategoriesFilters", new[] { "SubcategoryId" });
            DropIndex("dbo.UserSubcategoriesFilters", new[] { "UserId" });
            DropIndex("dbo.UserSizesFilters", new[] { "SizeId" });
            DropIndex("dbo.UserSizesFilters", new[] { "UserId" });
            DropIndex("dbo.UserBrandFilters", new[] { "BrandId" });
            DropIndex("dbo.UserBrandFilters", new[] { "UserId" });
            DropIndex("dbo.Purchases", new[] { "PromotionId" });
            DropIndex("dbo.Purchases", new[] { "ClothingItemSizeId" });
            DropIndex("dbo.Purchases", new[] { "UserId" });
            DropIndex("dbo.ClothingItemsPromotions", new[] { "PromotionId" });
            DropIndex("dbo.ClothingItemsPromotions", new[] { "ClothingItemId" });
            DropIndex("dbo.Users", new[] { "ClubCardId" });
            DropIndex("dbo.ClothingItems", new[] { "BrandId" });
            DropIndex("dbo.ClothingItemsSizes", new[] { "ShopId" });
            DropIndex("dbo.ClothingItemsSizes", new[] { "SizeId" });
            DropIndex("dbo.ClothingItemsSizes", new[] { "ClothingItemId" });
            DropIndex("dbo.Carts", new[] { "ClothingItemSizeId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.AdministratorsPermissions", new[] { "PermissionId" });
            DropIndex("dbo.AdministratorsPermissions", new[] { "AdministratorId" });
            DropPrimaryKey("dbo.ClubCards");
            AlterColumn("dbo.Sizes", "Size", c => c.Int(nullable: false));
            AlterColumn("dbo.ClubCards", "DiscountPercantage", c => c.Double());
            AlterColumn("dbo.ClubCards", "Id", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.Sizes", "SizeType");
            DropColumn("dbo.Promotions", "EndDate");
            DropColumn("dbo.Promotions", "StartDate");
            DropColumn("dbo.Promotions", "Name");
            DropColumn("dbo.Permissions", "Name");
            DropColumn("dbo.Users", "ClubCardId");
            DropColumn("dbo.ClothingItems", "DateAdded");
            DropColumn("dbo.ClothingItems", "SizeType");
            DropColumn("dbo.ClothingItems", "Price");
            DropColumn("dbo.ClothingItems", "Description");
            DropColumn("dbo.ClothingItems", "BrandId");
            DropColumn("dbo.Administrators", "Enabled");
            DropTable("dbo.UserSubcategoriesFilters");
            DropTable("dbo.UserSizesFilters");
            DropTable("dbo.UserBrandFilters");
            DropTable("dbo.Purchases");
            DropTable("dbo.ClothingItemsPromotions");
            DropTable("dbo.ClothingItemsSizes");
            DropTable("dbo.Carts");
            DropTable("dbo.Brands");
            DropTable("dbo.AdministratorsPermissions");
            AddPrimaryKey("dbo.ClubCards", "Id");
            RenameIndex(table: "dbo.ClothingItems", name: "IX_SubcategoryId", newName: "IX_CategoryId");
            RenameColumn(table: "dbo.ClothingItems", name: "SubcategoryId", newName: "CategoryId");
            CreateIndex("dbo.SizeTypes", "ClothingItemId");
            CreateIndex("dbo.Sizes", "SizeTypeId");
            CreateIndex("dbo.Promotions", "ClothingItemId");
            AddForeignKey("dbo.ClubCards", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sizes", "SizeTypeId", "dbo.SizeTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SizeTypes", "ClothingItemId", "dbo.ClothingItems", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Promotions", "ClothingItemId", "dbo.ClothingItems", "Id", cascadeDelete: true);
        }
    }
}
