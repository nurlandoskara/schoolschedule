using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public class GroupsController : BaseController<Group>
    {
        public GroupsController()
        {
            Context = new ModelContainer();
        }
    }
}
