namespace ClothingShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeForeignKeyForClubcardsNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards");
            DropIndex("dbo.Users", new[] { "ClubCardId" });
            AlterColumn("dbo.Users", "ClubCardId", c => c.Guid());
            CreateIndex("dbo.Users", "ClubCardId");
            AddForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards");
            DropIndex("dbo.Users", new[] { "ClubCardId" });
            AlterColumn("dbo.Users", "ClubCardId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "ClubCardId");
            AddForeignKey("dbo.Users", "ClubCardId", "dbo.ClubCards", "Id", cascadeDelete: true);
        }
    }
}
