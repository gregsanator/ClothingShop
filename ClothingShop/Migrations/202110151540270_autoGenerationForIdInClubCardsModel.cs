namespace ClothingShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoGenerationForIdInClubCardsModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards");
            DropPrimaryKey("dbo.ClubCards");
            AlterColumn("dbo.ClubCards", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.ClubCards", "Id");
            AddForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards");
            DropPrimaryKey("dbo.ClubCards");
            AlterColumn("dbo.ClubCards", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ClubCards", "Id");
            AddForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards", "Id");
        }
    }
}
