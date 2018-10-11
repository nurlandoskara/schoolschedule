using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public class LessonsController : BaseController<Lesson>
    {
        public LessonsController()
        {
            Context = new ModelContainer();
        }

        public ActionResult Index(int id)
        {
            var lessons = Context.Lessons.Where(x => x.SubjectGroup.GroupId == id).Include(l => l.SubjectGroup).Include(l => l.SubjectTeacher);
            return View(lessons.ToList());
        }

        public ActionResult Create(int id)
        {
            var subjects = Context.SubjectGroups.Where(x => x.GroupId == id).Include(x => x.Subject);
            var subjectIds = subjects.Select(x => x.SubjectId).ToList();
            ViewBag.SubjectGroupId = new SelectList(subjects, "Id", "Subject.DisplayName");
            ViewBag.SubjectTeacherId = new SelectList(Context.SubjectTeachers.Where(x => subjectIds.Contains(x.SubjectId)).Include(x => x.Teacher), "Id", "Teacher.DisplayName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(Lesson entity)
        {
            if (ModelState.IsValid)
            {
                Context.Lessons.Add(entity);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectGroupId = new SelectList(Context.SubjectGroups, "Id", "Id", entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = new SelectList(Context.SubjectTeachers, "Id", "Id", entity.SubjectTeacherId);
            return View(entity);
        }
        
        public override ActionResult Edit(int id)
        {
            var entity = Context.Lessons.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectGroupId = new SelectList(Context.SubjectGroups, "Id", "Id", entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = new SelectList(Context.SubjectTeachers, "Id", "Id", entity.SubjectTeacherId);
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
                return RedirectToAction("Index");
            }
            ViewBag.SubjectGroupId = new SelectList(Context.SubjectGroups, "Id", "Id", entity.SubjectGroupId);
            ViewBag.SubjectTeacherId = new SelectList(Context.SubjectTeachers, "Id", "Id", entity.SubjectTeacherId);
            return View(entity);
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
