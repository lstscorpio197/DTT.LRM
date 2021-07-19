namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_LiquidationEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Liquidation", "Creator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Liquidation", "Creator");
        }
    }
}
