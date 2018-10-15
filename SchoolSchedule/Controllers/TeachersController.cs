using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public class TeachersController : BaseController<Teacher>
    {
        public TeachersController()
        {
            Context = new ModelContainer();
        }
    }
}
