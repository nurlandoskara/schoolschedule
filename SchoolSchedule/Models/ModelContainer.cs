namespace SchoolSchedule.Models
{
    using System.Data.Entity;

    public class ModelContainer : DbContext
    {
        // Your context has been configured to use a 'ModelContainer' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SchoolSchedule.Models.ModelContainer' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelContainer' 
        // connection string in the application configuration file.
        public ModelContainer()
            : base("name=ModelContainer")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }
        public virtual DbSet<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Auditory> Auditories { get; set; }
        public virtual DbSet<News> Newses { get; set; }
    }
}