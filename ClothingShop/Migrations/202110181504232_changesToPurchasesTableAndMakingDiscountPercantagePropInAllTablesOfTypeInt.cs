namespace ClothingShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesToPurchasesTableAndMakingDiscountPercantagePropInAllTablesOfTypeInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "PromotionId", "dbo.Promotions");
            DropIndex("dbo.Purchases", new[] { "PromotionId" });
            AlterColumn("dbo.ClubCards", "DiscountPercantage", c => c.Int(nullable: false));
            AlterColumn("dbo.Promotions", "DiscountPercantage", c => c.Int(nullable: false));
            AlterColumn("dbo.Purchases", "DiscountPercantage", c => c.Int());
            DropColumn("dbo.Purchases", "PromotionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "PromotionId", c => c.Guid());
            AlterColumn("dbo.Purchases", "DiscountPercantage", c => c.Double());
            AlterColumn("dbo.Promotions", "DiscountPercantage", c => c.Double(nullable: false));
            AlterColumn("dbo.ClubCards", "DiscountPercantage", c => c.Double(nullable: false));
            CreateIndex("dbo.Purchases", "PromotionId");
            AddForeignKey("dbo.Purchases", "PromotionId", "dbo.Promotions", "Id");
        }
    }
}
