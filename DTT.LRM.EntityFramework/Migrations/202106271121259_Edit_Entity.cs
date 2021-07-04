namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Entity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookCategory", "TotalBorrowTime", c => c.Int(nullable: false));
            DropColumn("dbo.BookReaderUsing", "BookStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookReaderUsing", "BookStatus", c => c.Int());
            AlterColumn("dbo.BookCategory", "TotalBorrowTime", c => c.Int());
        }
    }
}
