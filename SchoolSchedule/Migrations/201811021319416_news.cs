namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        ClassYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
