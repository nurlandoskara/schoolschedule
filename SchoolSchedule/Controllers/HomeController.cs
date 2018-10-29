using System.Collections.Generic;
using System.Data.Entity;
using SchoolSchedule.Helpers;
using SchoolSchedule.Models;
using SchoolSchedule.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace SchoolSchedule.Controllers
{
    public class HomeController : BaseController<Lesson>
    {
        public HomeController()
        {
            Context = new ModelContainer();
        }

        [AllowAnonymous]
        public ActionResult Schedule(int? groupId)
        {
            groupId = groupId ?? GetGroups().FirstOrDefault()?.Id;
            var lessons = new List<Lesson>();
            if (groupId != null)
            {
                lessons = Context.Lessons.Where(x => !x.IsDeleted && x.SubjectGroup.GroupId == groupId).Include(l => l.SubjectGroup).Include(l => l.SubjectGroup.Subject)
                    .Include(l => l.SubjectGroup.Group).Include(l => l.SubjectTeacher)
                    .Include(l => l.SubjectTeacher.Teacher).Include(x => x.Auditory).ToList();
            }

            var model = new LessonViewModel()
            {
                GroupId = groupId,
                Lessons = lessons,
                Groups = GetGroups().GetSelectableList(groupId)
            };
            return View("Index", model);
        }
    }
}