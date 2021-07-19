namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_BookLiquidationEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookLiquidation", "BookId", "dbo.Books");
            DropIndex("dbo.BookLiquidation", new[] { "BookId" });
            AddColumn("dbo.BookLiquidation", "BookCategorieId", c => c.Int(nullable: false));
            CreateIndex("dbo.BookLiquidation", "BookCategorieId");
            AddForeignKey("dbo.BookLiquidation", "BookCategorieId", "dbo.BookCategory", "Id", cascadeDelete: true);
            DropColumn("dbo.BookLiquidation", "BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookLiquidation", "BookId", c => c.Int(nullable: false));
            DropForeignKey("dbo.BookLiquidation", "BookCategorieId", "dbo.BookCategory");
            DropIndex("dbo.BookLiquidation", new[] { "BookCategorieId" });
            DropColumn("dbo.BookLiquidation", "BookCategorieId");
            CreateIndex("dbo.BookLiquidation", "BookId");
            AddForeignKey("dbo.BookLiquidation", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
