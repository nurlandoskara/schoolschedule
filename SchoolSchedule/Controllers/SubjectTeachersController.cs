using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolSchedule.Models;
using SchoolSchedule.ViewModels;

namespace SchoolSchedule.Controllers
{
    public class SubjectTeachersController : BaseController<SubjectTeacher>
    {
        public SubjectTeachersController()
        {
            Context = new ModelContainer();
        }

        public override ActionResult Index()
        {
            var entities = Context.SubjectTeachers.Include(x => x.Subject).Include(x => x.Teacher);
            var model = new SubjectTeacherViewModel
            {
                ClassYear = 0,
                SubjectTeachers = entities,
                ClassYearses = Enum.GetValues(typeof(ClassYears)).Cast<ClassYears>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList()
            };
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult IndexByGroup(ClassYears classYear)
        {
            var entities = Context.SubjectTeachers.Where(x => x.Subject.ClassYear == classYear).Include(x => x.Subject).Include(x => x.Teacher);
            var model = new SubjectTeacherViewModel
            {
                ClassYear = classYear,
                SubjectTeachers = entities,
                ClassYearses = Enum.GetValues(typeof(ClassYears)).Cast<ClassYears>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int) v).ToString()
                }).ToList()
            };
            return View("Index", model);
        }
        
        public ActionResult CreateByGroup(ClassYears classYear)
        {
            ViewBag.SubjectId = new SelectList(GetSubjects(classYear), "Id", "DisplayName");
            ViewBag.TeacherId = new SelectList(GetTeachers(), "Id", "DisplayName");
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(SubjectTeacher entity)
        {
            if (ModelState.IsValid)
            {
                Context.SubjectTeachers.Add(entity);
                Context.SaveChanges();
                return RedirectToAction("IndexByGroup", new { entity.Subject.ClassYear });
            }

            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Subject.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.TeacherId = new SelectList(GetTeachers(), "Id", "DisplayName", entity.TeacherId);
            return View(entity);
        }
        
        public override ActionResult Edit(int id)
        {
            var entity = Context.SubjectTeachers.Include(x => x.Subject).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Subject.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.TeacherId = new SelectList(GetTeachers(), "Id", "DisplayName", entity.TeacherId);
            return View(entity);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(SubjectTeacher entity)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Subject.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.TeacherId = new SelectList(GetTeachers(), "Id", "DisplayName", entity.TeacherId);
            return View(entity);
        }
    }
}
