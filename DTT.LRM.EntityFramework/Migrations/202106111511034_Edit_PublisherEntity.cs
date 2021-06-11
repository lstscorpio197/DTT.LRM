namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_PublisherEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publisher", "Code", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publisher", "Code", c => c.Int(nullable: false));
        }
    }
}
