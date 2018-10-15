using SchoolSchedule.Models;
using SchoolSchedule.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SchoolSchedule.Controllers
{
    public class SubjectTeachersController : BaseController<SubjectTeacher>
    {
        public SubjectTeachersController()
        {
            Context = new ModelContainer();
        }
        
        public ActionResult IndexByClass(ClassYears? classYear)
        {
            var entities = (classYear != null)
                ? Context.SubjectTeachers.Where(x => x.Subject.ClassYear == classYear).Include(x => x.Subject)
                    .Include(x => x.Teacher)
                : Context.SubjectTeachers.Include(x => x.Subject).Include(x => x.Teacher);
            var model = new SubjectTeacherViewModel
            {
                ClassYear = classYear,
                SubjectTeachers = entities
            };
            return View("Index", model);
        }
        
        public override ActionResult Create(ClassYears? classYear)
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
                var classYear = Context.SubjectTeachers.Include(x => x.Subject).FirstOrDefault(x => x.Id == entity.Id)
                    ?.Subject.ClassYear;
                return RedirectToAction("IndexByClass", new { classYear });
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
                var classYear = Context.SubjectTeachers.Include(x => x.Subject).FirstOrDefault(x => x.Id == entity.Id)?.Subject.ClassYear;
                return RedirectToAction("IndexByClass", new { classYear });
            }
            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Subject.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.TeacherId = new SelectList(GetTeachers(), "Id", "DisplayName", entity.TeacherId);
            return View(entity);
        }

        public override ActionResult Delete(int id)
        {
            var entity = Context.Set<SubjectTeacher>().Include(x => x.Subject).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            entity.IsDeleted = true;
            Context.SaveChanges();
            return RedirectToAction("IndexByClass", new { entity.Subject.ClassYear });
        }
    }
}
