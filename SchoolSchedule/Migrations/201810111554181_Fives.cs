namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fives : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "ClassYear", c => c.Int(nullable: false));
            AddColumn("dbo.SubjectGroups", "ClassYear", c => c.Int(nullable: false));
            AddColumn("dbo.SubjectTeachers", "ClassYear", c => c.Int(nullable: false));
            AddColumn("dbo.Teachers", "ClassYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "ClassYear");
            DropColumn("dbo.SubjectTeachers", "ClassYear");
            DropColumn("dbo.SubjectGroups", "ClassYear");
            DropColumn("dbo.Lessons", "ClassYear");
        }
    }
}
