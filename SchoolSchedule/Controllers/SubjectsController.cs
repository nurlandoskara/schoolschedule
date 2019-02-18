using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public class SubjectsController : BaseController<Subject>
    {
        public SubjectsController()
        {
            Context = new ApplicationDbContext();
        }

    }
}
