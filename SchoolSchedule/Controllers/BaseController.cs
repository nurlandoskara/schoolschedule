﻿using SchoolSchedule.Models;
using SchoolSchedule.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SchoolSchedule.Controllers
{
    public abstract class BaseController<T> : Controller where T: BaseDbObject, new()
    {
        protected ModelContainer Context;

        public virtual ActionResult Index(ClassYears? classYear)
        {
            var entities = (classYear != null) ? Context.Set<T>().Where(x => x.ClassYear == classYear).ToList() : Context.Set<T>().ToList();
            var model = new BaseViewModel<T>()
            {
                ClassYear = classYear,
                List = entities
            };
            return View("Index", model);
        }

        public virtual ActionResult Create(ClassYears? classYear)
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(T entity)
        {
            if (!ModelState.IsValid) return View(entity);
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return RedirectToAction("Index", new { entity.ClassYear});
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
            return RedirectToAction("Index", new { entity.ClassYear });
        }
        
        public virtual ActionResult Delete(int id)
        {
            var entity = Context.Set<T>().Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            entity.IsDeleted = true;
            Context.SaveChanges();
            return RedirectToAction("Index", new { entity.ClassYear });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public IEnumerable<Subject> GetSubjects(ClassYears? classYear)
        {
            return (classYear != null)
                ? Context.Subjects.Where(x => !x.IsDeleted && x.ClassYear == classYear).ToList()
                : Context.Subjects.Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return Context.Teachers.Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<Group> GetGroups()
        {
            return Context.Groups.Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<SubjectGroup> GetSubjectGroups(int? groupId)
        {
            var subjectGroups = Context.SubjectGroups.Include(x => x.Subject).Include(x => x.Group).Where(x => !x.IsDeleted);
            return (groupId != null)
                ? subjectGroups.Where(x => x.GroupId == groupId)
                : subjectGroups;
        }

        public IEnumerable<SubjectTeacher> GetSubjectTeachers(int? groupId)
        {
            var subjects = Context.SubjectGroups.Where(x => !x.IsDeleted);
            var subjectIds = (groupId != null)
                ? subjects.Where(x => x.GroupId == groupId).Select(x => x.SubjectId).ToList()
                : subjects.Select(x => x.SubjectId).ToList();
            return Context.SubjectTeachers.Where(x => !x.IsDeleted && subjectIds.Contains(x.SubjectId)).Include(x => x.Subject).Include(x => x.Teacher);
        }
    }
}