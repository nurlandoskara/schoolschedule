using SchoolSchedule.Helpers;
using SchoolSchedule.Models;
using SchoolSchedule.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SchoolSchedule.Controllers
{
    public class LessonsController : BaseController<Lesson>
    {
        public LessonsController()
        {
            Context = new ModelContainer();
        }

        public ActionResult IndexByGroup(int? groupId)
        {
            groupId = groupId ?? GetGroups().FirstOrDefault()?.Id;
            var lessons = Context.Lessons.Include(l => l.SubjectGroup).Include(l => l.SubjectGroup.Subject)
                .Include(l => l.SubjectGroup.Group).Include(l => l.SubjectTeacher)
                .Include(l => l.SubjectTeacher.Teacher);
            lessons = (groupId != null)
                ? lessons.Where(x => x.SubjectGroup.GroupId == groupId)
                : lessons;
            var model = new LessonViewModel()
            {
                GroupId = groupId,
                Lessons = lessons.OrderBy(x => x.DayOfWeek).ThenBy(x => x.Order).ToList(),
                Groups = GetGroups().GetSelectableList(groupId)
            };
            return View("Index", model);
        }

        public ActionResult CreateByGroup(int? groupId)
        {
            var classYear = Context.Groups.Find(groupId)?.ClassYear;
            ViewBag.SubjectGroupId = GetSubjectGroups(groupId).GetSelectableSubjectGroups();
            ViewBag.SubjectTeacherId = GetSubjectTeachers(classYear).GetSelectableSubjectTeachers();
            return View("Create");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(Lesson entity)
        {
            if (ModelState.IsValid)
            {
                Context.Lessons.Add(entity);
                Context.SaveChanges();
                var groupId = Context.SubjectGroups.Include(x => x.Group).FirstOrDefault(x => x.Id == entity.SubjectGroupId)?.Group.Id;
                return RedirectToAction("IndexByGroup", new { groupId });
            }
            
            ViewBag.SubjectGroupId = GetSubjectGroups(entity.SubjectGroup.Group.Id).GetSelectableSubjectGroups(entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = GetSubjectTeachers(entity.SubjectGroup.Group.ClassYear).GetSelectableSubjectTeachers(entity.SubjectTeacherId);
            return View(entity);
        }
        
        public override ActionResult Edit(int id)
        {
            var entity = Context.Lessons.Include(x => x.SubjectGroup).Include(x => x.SubjectGroup.Group).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectGroupId = GetSubjectGroups(entity.SubjectGroup.GroupId).GetSelectableSubjectGroups(entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = GetSubjectTeachers(entity.SubjectGroup.Group.ClassYear).GetSelectableSubjectTeachers(entity.SubjectTeacherId);
            return View(entity);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(Lesson entity)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                var groupId = Context.SubjectGroups.Include(x => x.Group).FirstOrDefault(x => x.Id == entity.SubjectGroupId)?.Group.Id;
                return RedirectToAction("IndexByGroup", new { groupId });
            }

            ViewBag.SubjectGroupId = GetSubjectGroups(entity.SubjectGroup.Group.Id).GetSelectableSubjectGroups(entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = GetSubjectTeachers(entity.SubjectGroup.Group.ClassYear).GetSelectableSubjectTeachers(entity.SubjectTeacherId);
            return View(entity);
        }

        public override ActionResult Delete(int id)
        {
            var entity = Context.Lessons.Include(x => x.SubjectGroup).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            entity.IsDeleted = true;
            Context.SaveChanges();
            return RedirectToAction("IndexByGroup", new { entity.SubjectGroup.GroupId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
