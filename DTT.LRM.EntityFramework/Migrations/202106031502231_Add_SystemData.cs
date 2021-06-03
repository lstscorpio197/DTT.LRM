namespace DTT.LRM.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SystemData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        BookFieldId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BookCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookField", t => t.BookFieldId, cascadeDelete: true)
                .Index(t => t.BookFieldId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.BookField",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        BookClassifyId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BookField_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookClassify", t => t.BookClassifyId, cascadeDelete: true)
                .Index(t => t.BookClassifyId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.BookClassify",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BookClassify_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 32),
                        OrganizationUnitId = c.Long(nullable: false),
                        PositionId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        DayOfBirth = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(maxLength: 15),
                        Email = c.String(),
                        Note = c.String(),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Employee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.OrganizationUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.OrganizationUnitId)
                .Index(t => t.PositionId)
                .Index(t => t.UserId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Position_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.PositionQuota",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionId = c.Int(nullable: false),
                        BookFieldId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
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
                    { "DynamicFilter_PositionQuota_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookField", t => t.BookFieldId, cascadeDelete: true)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.PositionId)
                .Index(t => t.BookFieldId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Publisher_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Reader",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 32),
                        OrganizationUnitId = c.Long(nullable: false),
                        PositionId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        DayOfBirth = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(maxLength: 15),
                        Email = c.String(),
                        Note = c.String(),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Reader_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.OrganizationUnitId, cascadeDelete: true)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.OrganizationUnitId)
                .Index(t => t.PositionId)
                .Index(t => t.UserId)
                .Index(t => t.IsDeleted);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reader", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Reader", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Reader", "OrganizationUnitId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.PositionQuota", "PositionId", "dbo.Position");
            DropForeignKey("dbo.PositionQuota", "BookFieldId", "dbo.BookField");
            DropForeignKey("dbo.Employee", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Employee", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Employee", "OrganizationUnitId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.BookCategory", "BookFieldId", "dbo.BookField");
            DropForeignKey("dbo.BookField", "BookClassifyId", "dbo.BookClassify");
            DropIndex("dbo.Reader", new[] { "IsDeleted" });
            DropIndex("dbo.Reader", new[] { "UserId" });
            DropIndex("dbo.Reader", new[] { "PositionId" });
            DropIndex("dbo.Reader", new[] { "OrganizationUnitId" });
            DropIndex("dbo.Publisher", new[] { "IsDeleted" });
            DropIndex("dbo.PositionQuota", new[] { "IsDeleted" });
            DropIndex("dbo.PositionQuota", new[] { "BookFieldId" });
            DropIndex("dbo.PositionQuota", new[] { "PositionId" });
            DropIndex("dbo.Position", new[] { "IsDeleted" });
            DropIndex("dbo.Employee", new[] { "IsDeleted" });
            DropIndex("dbo.Employee", new[] { "UserId" });
            DropIndex("dbo.Employee", new[] { "PositionId" });
            DropIndex("dbo.Employee", new[] { "OrganizationUnitId" });
            DropIndex("dbo.BookClassify", new[] { "IsDeleted" });
            DropIndex("dbo.BookField", new[] { "IsDeleted" });
            DropIndex("dbo.BookField", new[] { "BookClassifyId" });
            DropIndex("dbo.BookCategory", new[] { "IsDeleted" });
            DropIndex("dbo.BookCategory", new[] { "BookFieldId" });
            DropTable("dbo.Reader",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Reader_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Publisher",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Publisher_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.PositionQuota",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PositionQuota_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Position",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Position_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Employee",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Employee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BookClassify",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookClassify_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BookField",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookField_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BookCategory",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BookCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
