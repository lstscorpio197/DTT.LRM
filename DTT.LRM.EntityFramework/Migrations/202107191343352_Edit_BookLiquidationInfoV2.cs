namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_BookLiquidationInfoV2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookLiquidation", "PublisherId", "dbo.Publisher");
            DropIndex("dbo.BookLiquidation", new[] { "PublisherId" });
            AlterColumn("dbo.BookLiquidation", "ReleaseYear", c => c.Int());
            AlterColumn("dbo.BookLiquidation", "PublisherId", c => c.Int());
            CreateIndex("dbo.BookLiquidation", "PublisherId");
            AddForeignKey("dbo.BookLiquidation", "PublisherId", "dbo.Publisher", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookLiquidation", "PublisherId", "dbo.Publisher");
            DropIndex("dbo.BookLiquidation", new[] { "PublisherId" });
            AlterColumn("dbo.BookLiquidation", "PublisherId", c => c.Int(nullable: false));
            AlterColumn("dbo.BookLiquidation", "ReleaseYear", c => c.Int(nullable: false));
            CreateIndex("dbo.BookLiquidation", "PublisherId");
            AddForeignKey("dbo.BookLiquidation", "PublisherId", "dbo.Publisher", "Id", cascadeDelete: true);
        }
    }
}
