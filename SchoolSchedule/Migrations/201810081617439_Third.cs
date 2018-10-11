namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "NameRu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "NameRu");
        }
    }
}
