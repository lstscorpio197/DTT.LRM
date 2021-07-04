namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditBookReaderUsingEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookReaderUsing", "GiveBackDateExpect", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookReaderUsing", "GiveBackDateExpect");
        }
    }
}
