namespace ClothingShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeFromDoubleToIntInModelsThatHaveDiscountPercantageProp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Purchases", "DiscountPercantage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Purchases", "DiscountPercantage", c => c.Int());
        }
    }
}
