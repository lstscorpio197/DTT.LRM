namespace DTT.LRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_BorrowEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Borrow", "EmployeeId", "dbo.Employee");
            DropIndex("dbo.Borrow", new[] { "EmployeeId" });
            AddColumn("dbo.Borrow", "Creator", c => c.String());
            AlterColumn("dbo.Borrow", "EmployeeId", c => c.Int());
            CreateIndex("dbo.Borrow", "EmployeeId");
            AddForeignKey("dbo.Borrow", "EmployeeId", "dbo.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrow", "EmployeeId", "dbo.Employee");
            DropIndex("dbo.Borrow", new[] { "EmployeeId" });
            AlterColumn("dbo.Borrow", "EmployeeId", c => c.Int(nullable: false));
            DropColumn("dbo.Borrow", "Creator");
            CreateIndex("dbo.Borrow", "EmployeeId");
            AddForeignKey("dbo.Borrow", "EmployeeId", "dbo.Employee", "Id", cascadeDelete: true);
        }
    }
}
