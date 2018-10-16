using SchoolSchedule.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SchoolSchedule.Helpers;
using SchoolSchedule.ViewModels;

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
            var lessons = Context.Lessons.Include(l => l.SubjectGroup).Include(l => l.SubjectGroup.Subject)
                .Include(l => l.SubjectGroup.Group).Include(l => l.SubjectTeacher)
                .Include(l => l.SubjectTeacher.Teacher);
            lessons = (groupId != null)
                ? lessons.Where(x => x.SubjectGroup.GroupId == groupId)
                : lessons;
            var model = new LessonViewModel()
            {
                GroupId = groupId,
                Lessons = lessons.ToList(),
                Groups = GetGroups().GetSelectableGroups(groupId)
            };
            return View("Index", model);
        }

        public ActionResult CreateByGroup(int? groupId)
        {
            ViewBag.SubjectGroupId = GetSubjectGroups(groupId).GetSelectableSubjectGroups();
            ViewBag.SubjectTeacherId = GetSubjectTeachers(groupId).GetSelectableSubjectTeachers();
            return View("Create");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(Lesson entity)
        {
            var groupId = Context.SubjectGroups.FirstOrDefault(x => x.Id == entity.SubjectGroupId)?.GroupId;
            if (ModelState.IsValid)
            {
                Context.Lessons.Add(entity);
                Context.SaveChanges();
                return RedirectToAction("IndexByGroup", new { groupId });
            }

            ViewBag.SubjectGroupId = GetSubjectGroups(groupId).GetSelectableSubjectGroups(entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = GetSubjectTeachers(groupId).GetSelectableSubjectTeachers(entity.SubjectTeacherId);
            return View(entity);
        }
        
        public override ActionResult Edit(int id)
        {
            var entity = Context.Lessons.Include(x => x.SubjectGroup).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectGroupId = GetSubjectGroups(entity.SubjectGroup.GroupId).GetSelectableSubjectGroups(entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = GetSubjectTeachers(entity.SubjectGroup.GroupId).GetSelectableSubjectTeachers(entity.SubjectTeacherId);
            return View(entity);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(Lesson entity)
        {
            var groupId = Context.SubjectGroups.FirstOrDefault(x => x.Id == entity.SubjectGroupId)?.GroupId;
            if (ModelState.IsValid)
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("IndexByGroup", new {groupId});
            }

            ViewBag.SubjectGroupId = GetSubjectGroups(groupId).GetSelectableSubjectGroups(entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = GetSubjectTeachers(groupId).GetSelectableSubjectTeachers(entity.SubjectTeacherId);
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
