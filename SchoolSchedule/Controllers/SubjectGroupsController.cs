using SchoolSchedule.Helpers;
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
            Context = new ApplicationDbContext();
        }
        
        public ActionResult IndexByGroup(int? groupId)
        {
            groupId = groupId ?? GetGroups().FirstOrDefault()?.Id;
            var entities = (groupId != null)
                ? Context.SubjectGroups.Where(x => x.GroupId == groupId).Include(x => x.Subject)
                    .Include(x => x.Group).ToList()
                : Context.SubjectGroups.Include(x => x.Subject).Include(x => x.Group).ToList();
            var model = new SubjectGroupViewModel()
            {
                GroupId = groupId,
                SubjectGroups = entities,
                Groups = GetGroups().GetSelectableList(groupId)
            };
            return View("Index", model);
        }

        public ActionResult CreateByGroup(int? groupId)
        {
            var classYear = Context.Groups.FirstOrDefault(x => x.Id == groupId)?.ClassYear;
            ViewBag.SubjectId = GetSubjects(classYear).GetSelectableList();
            ViewBag.GroupId = GetGroups().GetSelectableList(groupId);
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

            ViewBag.SubjectId = GetSubjects(entity.Group.ClassYear).GetSelectableList(entity.SubjectId);
            ViewBag.GroupId = GetGroups().GetSelectableList(entity.GroupId);
            return View(entity);
        }

        public override ActionResult Edit(int id)
        {
            var entity = Context.SubjectGroups.Include(x => x.Subject).Include(x => x.Group).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = GetSubjects(entity.Group.ClassYear).GetSelectableList(entity.SubjectId);
            ViewBag.GroupId = GetGroups().GetSelectableList(entity.GroupId);
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
            ViewBag.SubjectId = GetSubjects(entity.Group.ClassYear).GetSelectableList(entity.SubjectId);
            ViewBag.GroupId = GetGroups().GetSelectableList(entity.GroupId);
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
