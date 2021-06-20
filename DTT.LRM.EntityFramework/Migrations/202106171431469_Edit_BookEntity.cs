namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_BookEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Code", c => c.Int(nullable: false));
        }
    }
}
