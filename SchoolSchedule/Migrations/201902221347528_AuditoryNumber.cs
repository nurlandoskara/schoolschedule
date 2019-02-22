namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditoryNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auditories", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auditories", "Number");
        }
    }
}
