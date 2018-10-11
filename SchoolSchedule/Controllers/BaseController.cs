using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public abstract class BaseController<T> : Controller where T: BaseDbObject, new()
    {
        protected ModelContainer Context;

        public virtual ActionResult Index()
        {
            var entities = Context.Set<T>();
            return View(entities);
        }

        public virtual ActionResult Details(int id)
        {
            var entity = Context.Set<T>().Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(T entity)
        {
            if (!ModelState.IsValid) return View(entity);
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public virtual ActionResult Edit(int id)
        {
            var entity = Context.Set<T>().Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(T entity)
        {
            if (!ModelState.IsValid) return View(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            var entity = Context.Set<T>().Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            entity.IsDeleted = true;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public IEnumerable<Subject> GetSubjects(ClassYears id)
        {
            return (id != 0)?Context.Subjects.Where(x => !x.IsDeleted && x.ClassYear == id): Context.Subjects.Where(x => !x.IsDeleted);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return Context.Teachers.Where(x => !x.IsDeleted);
        }

        public IEnumerable<Group> GetGroups()
        {
            return Context.Groups.Where(x => !x.IsDeleted);
        }
    }
}