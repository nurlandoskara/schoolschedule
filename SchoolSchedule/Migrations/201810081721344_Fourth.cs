namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        SubjectGroupId = c.Int(nullable: false),
                        SubjectTeacherId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectGroups", t => t.SubjectGroupId, cascadeDelete: false)
                .ForeignKey("dbo.SubjectTeachers", t => t.SubjectTeacherId, cascadeDelete: false)
                .Index(t => t.SubjectGroupId)
                .Index(t => t.SubjectTeacherId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "SubjectTeacherId", "dbo.SubjectTeachers");
            DropForeignKey("dbo.Lessons", "SubjectGroupId", "dbo.SubjectGroups");
            DropIndex("dbo.Lessons", new[] { "SubjectTeacherId" });
            DropIndex("dbo.Lessons", new[] { "SubjectGroupId" });
            DropTable("dbo.Lessons");
        }
    }
}
