namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_BookLiquidationInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookLiquidation", "Author", c => c.String());
            AddColumn("dbo.BookLiquidation", "ReleaseYear", c => c.Int(nullable: false));
            AddColumn("dbo.BookLiquidation", "PublisherId", c => c.Int(nullable: false));
            CreateIndex("dbo.BookLiquidation", "PublisherId");
            AddForeignKey("dbo.BookLiquidation", "PublisherId", "dbo.Publisher", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookLiquidation", "PublisherId", "dbo.Publisher");
            DropIndex("dbo.BookLiquidation", new[] { "PublisherId" });
            DropColumn("dbo.BookLiquidation", "PublisherId");
            DropColumn("dbo.BookLiquidation", "ReleaseYear");
            DropColumn("dbo.BookLiquidation", "Author");
        }
    }
}
