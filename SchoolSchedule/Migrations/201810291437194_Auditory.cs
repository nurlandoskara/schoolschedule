namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Auditory : DbMigration
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
            
            AddColumn("dbo.Lessons", "AuditoryId", c => c.Int());
            CreateIndex("dbo.Lessons", "AuditoryId");
            AddForeignKey("dbo.Lessons", "AuditoryId", "dbo.Auditories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "AuditoryId", "dbo.Auditories");
            DropIndex("dbo.Lessons", new[] { "AuditoryId" });
            DropColumn("dbo.Lessons", "AuditoryId");
            DropTable("dbo.Auditories");
        }
    }
}
