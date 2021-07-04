namespace DTT.LRM.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EntityBookUsing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookBorrow",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowId = c.Int(nullable: false),
                        BookId = c.Int(),
                        BookCategoryId = c.Int(),
                        Author = c.String(),
                        ReleaseYear = c.Int(),
                        PublisherId = c.Int(),
                        GiveBackDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookBorrow_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.BookCategory", t => t.BookCategoryId)
                .ForeignKey("dbo.Publisher", t => t.PublisherId)
                .Index(t => t.BookId)
                .Index(t => t.BookCategoryId)
                .Index(t => t.PublisherId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.BookGiveBack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GiveBackId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        GiveBackDate = c.DateTime(),
                        Note = c.String(),
                        Status = c.Int(nullable: false),
                        ViolateId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookGiveBack_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Violate", t => t.ViolateId)
                .Index(t => t.BookId)
                .Index(t => t.ViolateId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Violate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReaderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        ViolateError = c.Int(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Violate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Reader", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.ReaderId)
                .Index(t => t.BookId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.BookLiquidation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LiquidationId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        LiquidationPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookLiquidation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.BookReaderUsing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReaderId = c.Int(nullable: false),
                        BookId = c.Int(),
                        BorrowDate = c.DateTime(nullable: false),
                        GiveBackDate = c.DateTime(),
                        IsUse = c.Int(nullable: false),
                        BookStatus = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookReaderUsing_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Reader", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.ReaderId)
                .Index(t => t.BookId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Borrow",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        ReaderId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Borrow_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Reader", t => t.ReaderId, cascadeDelete: false)
                .Index(t => t.ReaderId)
                .Index(t => t.EmployeeId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.GiveBack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        ReaderId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        GiveBackDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GiveBack_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Reader", t => t.ReaderId, cascadeDelete: false)
                .Index(t => t.ReaderId)
                .Index(t => t.EmployeeId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Liquidation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Liquidation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            AddColumn("dbo.BookCategory", "TotalBorrowTime", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GiveBack", "ReaderId", "dbo.Reader");
            DropForeignKey("dbo.GiveBack", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Borrow", "ReaderId", "dbo.Reader");
            DropForeignKey("dbo.Borrow", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.BookReaderUsing", "ReaderId", "dbo.Reader");
            DropForeignKey("dbo.BookReaderUsing", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookLiquidation", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookGiveBack", "ViolateId", "dbo.Violate");
            DropForeignKey("dbo.Violate", "ReaderId", "dbo.Reader");
            DropForeignKey("dbo.Violate", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookGiveBack", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookBorrow", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.BookBorrow", "BookCategoryId", "dbo.BookCategory");
            DropForeignKey("dbo.BookBorrow", "BookId", "dbo.Books");
            DropIndex("dbo.Liquidation", new[] { "IsDeleted" });
            DropIndex("dbo.GiveBack", new[] { "IsDeleted" });
            DropIndex("dbo.GiveBack", new[] { "EmployeeId" });
            DropIndex("dbo.GiveBack", new[] { "ReaderId" });
            DropIndex("dbo.Borrow", new[] { "IsDeleted" });
            DropIndex("dbo.Borrow", new[] { "EmployeeId" });
            DropIndex("dbo.Borrow", new[] { "ReaderId" });
            DropIndex("dbo.BookReaderUsing", new[] { "IsDeleted" });
            DropIndex("dbo.BookReaderUsing", new[] { "BookId" });
            DropIndex("dbo.BookReaderUsing", new[] { "ReaderId" });
            DropIndex("dbo.BookLiquidation", new[] { "IsDeleted" });
            DropIndex("dbo.BookLiquidation", new[] { "BookId" });
            DropIndex("dbo.Violate", new[] { "IsDeleted" });
            DropIndex("dbo.Violate", new[] { "BookId" });
            DropIndex("dbo.Violate", new[] { "ReaderId" });
            DropIndex("dbo.BookGiveBack", new[] { "IsDeleted" });
            DropIndex("dbo.BookGiveBack", new[] { "ViolateId" });
            DropIndex("dbo.BookGiveBack", new[] { "BookId" });
            DropIndex("dbo.BookBorrow", new[] { "IsDeleted" });
            DropIndex("dbo.BookBorrow", new[] { "PublisherId" });
            DropIndex("dbo.BookBorrow", new[] { "BookCategoryId" });
            DropIndex("dbo.BookBorrow", new[] { "BookId" });
            DropColumn("dbo.BookCategory", "TotalBorrowTime");
            DropTable("dbo.Liquidation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Liquidation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.GiveBack",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GiveBack_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Borrow",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Borrow_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BookReaderUsing",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookReaderUsing_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BookLiquidation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookLiquidation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Violate",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Violate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BookGiveBack",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookGiveBack_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BookBorrow",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookBorrow_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
