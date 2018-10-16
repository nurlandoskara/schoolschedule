using SchoolSchedule.Models;
using SchoolSchedule.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SchoolSchedule.Controllers
{
    public class SubjectGroupsController : BaseController<SubjectGroup>
    {
        public SubjectGroupsController()
        {
            Context = new ModelContainer();
        }
        
        public ActionResult IndexByGroup(int? groupId)
        {
            var entities = (groupId != null)
                ? Context.SubjectGroups.Where(x => x.GroupId == groupId).Include(x => x.Subject)
                    .Include(x => x.Group).ToList()
                : Context.SubjectGroups.Include(x => x.Subject).Include(x => x.Group).ToList();
            var model = new SubjectGroupViewModel()
            {
                GroupId = groupId,
                SubjectGroups = entities,
                Groups = Context.Groups.Where(x => !x.IsDeleted)
                    .Select(v => new SelectListItem {Text = string.Concat(v.ClassYear, v.ClassLetter), Value = v.Id.ToString()}).ToList()
            };
            return View("Index", model);
        }

        public ActionResult CreateByGroup(int? groupId)
        {
            var classYear = Context.Groups.FirstOrDefault(x => x.Id == groupId)?.ClassYear;
            ViewBag.SubjectId = new SelectList(GetSubjects(classYear), "Id", "DisplayName");
            ViewBag.GroupId = new SelectList(GetGroups(), "Id", "DisplayName");
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
            ViewBag.GroupId = new SelectList(GetGroups(), "Id", "DisplayName", entity.GroupId);
            return View(entity);
        }

        public override ActionResult Edit(int id)
        {
            var entity = Context.SubjectGroups.Include(x => x.Subject).Include(x => x.Group).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Group.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.GroupId = new SelectList(GetGroups(), "Id", "DisplayName");
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
                return RedirectToAction("IndexByGroup", new { entity.GroupId });
            }
            ViewBag.SubjectId = new SelectList(GetSubjects(entity.Group.ClassYear), "Id", "DisplayName", entity.SubjectId);
            ViewBag.GroupId = new SelectList(GetGroups(), "Id", "DisplayName", entity.GroupId);
            return View(entity);
        }

        public override ActionResult Delete(int id)
        {
            var entity = Context.Set<SubjectGroup>().Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            entity.IsDeleted = true;
            Context.SaveChanges();
            return RedirectToAction("IndexByGroup", new { entity.GroupId });
        }
    }
}
