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
    public class SubjectGroupsController : BaseController<SubjectGroup>
    {
        public SubjectGroupsController()
        {
            Context = new ModelContainer();
        }

        public override ActionResult Index()
        {
            var entities = Context.SubjectGroups.Include(x => x.Subject).Include(x => x.Group);
            var model = new SubjectGroupViewModel()
            {
                GroupId = 1,
                SubjectGroups = entities,
                Groups = Context.Groups.Where(x => !x.IsDeleted)
                    .Select(v => new SelectListItem {Text = string.Concat(v.ClassYear,v.ClassLetter), Value = v.Id.ToString()}).ToList()
            };
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult IndexByGroup(int groupId)
        {
            var group = Context.Groups.FirstOrDefault(x => x.Id == groupId);
            var entities = Context.SubjectGroups.Where(x => x.Subject.ClassYear == group.ClassYear).Include(x => x.Subject).Include(x => x.Group);
            var model = new SubjectGroupViewModel()
            {
                GroupId = groupId,
                SubjectGroups = entities,
                Groups = Context.Groups.Where(x => !x.IsDeleted)
                    .Select(v => new SelectListItem {Text = string.Concat(v.ClassYear, v.ClassLetter), Value = v.Id.ToString()}).ToList()
            };
            return View("Index", model);
        }

        public ActionResult CreateByGroup(int groupId)
        {
            var group = Context.Groups.FirstOrDefault(x => x.Id == groupId);
            ViewBag.SubjectId = new SelectList(GetSubjects(@group?.ClassYear ?? 0), "Id", "DisplayName");
            ViewBag.GroupId = groupId;
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(SubjectGroup entity)
        {
            if (ModelState.IsValid)
            {
                Context.SubjectGroups.Add(entity);
                Context.SaveChanges();
                return RedirectToAction("IndexByGroup", new { entity.GroupId});
            }

            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Group.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.GroupId = entity.GroupId;
            return View(entity);
        }

        public override ActionResult Edit(int id)
        {
            var entity = Context.SubjectGroups.Include(x => x.Subject).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Group.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.GroupId = entity.GroupId;
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(SubjectGroup entity)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Group.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.GroupId = entity.GroupId;
            return View(entity);
        }
    }
}
