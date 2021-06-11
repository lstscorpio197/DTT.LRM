namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_PositionQuota : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PositionQuota", "BookFieldId", "dbo.BookField");
            DropIndex("dbo.PositionQuota", new[] { "BookFieldId" });
            AddColumn("dbo.PositionQuota", "BookClassifyId", c => c.Int(nullable: false));
            CreateIndex("dbo.PositionQuota", "BookClassifyId");
            AddForeignKey("dbo.PositionQuota", "BookClassifyId", "dbo.BookClassify", "Id", cascadeDelete: true);
            DropColumn("dbo.PositionQuota", "BookFieldId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PositionQuota", "BookFieldId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PositionQuota", "BookClassifyId", "dbo.BookClassify");
            DropIndex("dbo.PositionQuota", new[] { "BookClassifyId" });
            DropColumn("dbo.PositionQuota", "BookClassifyId");
            CreateIndex("dbo.PositionQuota", "BookFieldId");
            AddForeignKey("dbo.PositionQuota", "BookFieldId", "dbo.BookField", "Id", cascadeDelete: true);
        }
    }
}
