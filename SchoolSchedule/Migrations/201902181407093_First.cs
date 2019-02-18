namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassLetter = c.String(),
                        CreatedYear = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        SubjectGroupId = c.Int(nullable: false),
                        SubjectTeacherId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        AuditoryId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditories", t => t.AuditoryId)
                .ForeignKey("dbo.SubjectGroups", t => t.SubjectGroupId, cascadeDelete: false)
                .ForeignKey("dbo.SubjectTeachers", t => t.SubjectTeacherId, cascadeDelete: false)
                .Index(t => t.SubjectGroupId)
                .Index(t => t.SubjectTeacherId)
                .Index(t => t.AuditoryId);
            
            CreateTable(
                "dbo.SubjectGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameKz = c.String(),
                        NameEn = c.String(),
                        NameRu = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Surname = c.String(),
                        PatronymicName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Lessons", "SubjectTeacherId", "dbo.SubjectTeachers");
            DropForeignKey("dbo.SubjectTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.SubjectTeachers", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Lessons", "SubjectGroupId", "dbo.SubjectGroups");
            DropForeignKey("dbo.SubjectGroups", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Lessons", "AuditoryId", "dbo.Auditories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubjectTeachers", new[] { "TeacherId" });
            DropIndex("dbo.SubjectTeachers", new[] { "SubjectId" });
            DropIndex("dbo.SubjectGroups", new[] { "GroupId" });
            DropIndex("dbo.SubjectGroups", new[] { "SubjectId" });
            DropIndex("dbo.Lessons", new[] { "AuditoryId" });
            DropIndex("dbo.Lessons", new[] { "SubjectTeacherId" });
            DropIndex("dbo.Lessons", new[] { "SubjectGroupId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.News");
            DropTable("dbo.Teachers");
            DropTable("dbo.SubjectTeachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectGroups");
            DropTable("dbo.Lessons");
            DropTable("dbo.Groups");
            DropTable("dbo.Auditories");
        }
    }
}
