using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public class NewsController : BaseController<News>
    {
        public NewsController()
        {
            Context = new ModelContainer();
        }
    }
}
