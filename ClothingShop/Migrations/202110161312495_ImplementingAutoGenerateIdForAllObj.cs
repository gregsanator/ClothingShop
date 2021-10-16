namespace ClothingShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementingAutoGenerateIdForAllObj : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClothingItems", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.UserBrandFilters", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Carts", "ClothingItemSizeId", "dbo.ClothingItemsSizes");
            DropForeignKey("dbo.Purchases", "ClothingItemSizeId", "dbo.ClothingItemsSizes");
            DropPrimaryKey("dbo.Brands");
            DropPrimaryKey("dbo.ClothingItemsSizes");
            DropPrimaryKey("dbo.ClothingItemsPromotions");
            DropPrimaryKey("dbo.Purchases");
            AlterColumn("dbo.Brands", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.ClothingItemsSizes", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.ClothingItemsPromotions", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Purchases", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Brands", "Id");
            AddPrimaryKey("dbo.ClothingItemsSizes", "Id");
            AddPrimaryKey("dbo.ClothingItemsPromotions", "Id");
            AddPrimaryKey("dbo.Purchases", "Id");
            AddForeignKey("dbo.ClothingItems", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserBrandFilters", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "ClothingItemSizeId", "dbo.ClothingItemsSizes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Purchases", "ClothingItemSizeId", "dbo.ClothingItemsSizes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "ClothingItemSizeId", "dbo.ClothingItemsSizes");
            DropForeignKey("dbo.Carts", "ClothingItemSizeId", "dbo.ClothingItemsSizes");
            DropForeignKey("dbo.UserBrandFilters", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.ClothingItems", "BrandId", "dbo.Brands");
            DropPrimaryKey("dbo.Purchases");
            DropPrimaryKey("dbo.ClothingItemsPromotions");
            DropPrimaryKey("dbo.ClothingItemsSizes");
            DropPrimaryKey("dbo.Brands");
            AlterColumn("dbo.Purchases", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ClothingItemsPromotions", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ClothingItemsSizes", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Brands", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Purchases", "Id");
            AddPrimaryKey("dbo.ClothingItemsPromotions", "Id");
            AddPrimaryKey("dbo.ClothingItemsSizes", "Id");
            AddPrimaryKey("dbo.Brands", "Id");
            AddForeignKey("dbo.Purchases", "ClothingItemSizeId", "dbo.ClothingItemsSizes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "ClothingItemSizeId", "dbo.ClothingItemsSizes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserBrandFilters", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClothingItems", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
        }
    }
}
