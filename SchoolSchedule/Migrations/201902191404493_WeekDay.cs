namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeekDay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "WeekDay", c => c.Int(nullable: false));
            DropColumn("dbo.Lessons", "DayOfWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lessons", "DayOfWeek", c => c.Int(nullable: false));
            DropColumn("dbo.Lessons", "WeekDay");
        }
    }
}
