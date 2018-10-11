namespace SchoolSchedule.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolSchedule.Models.ModelContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SchoolSchedule.Models.ModelContainer";
        }

        protected override void Seed(SchoolSchedule.Models.ModelContainer context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}