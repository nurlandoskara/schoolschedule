using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public class AuditoriesController : BaseController<Auditory>
    {
        public AuditoriesController()
        {
            Context = new ModelContainer();
        }
    }
}
