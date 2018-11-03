using SchoolSchedule.Models;
using SchoolSchedule.ViewModels;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;

namespace SchoolSchedule.Controllers
{
    public class NewsController : BaseController<News>
    {
        public NewsController()
        {
            Context = new ModelContainer();
        }

        public override ActionResult Create(int? classYear)
        {
            return View(new NewsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNews(NewsViewModel entityViewModel)
        {
            if (!ModelState.IsValid) return View("Create", entityViewModel);
            var entity = new News
            {
                Title = entityViewModel.Title,
                Description = entityViewModel.Description
            };

            if (entityViewModel.ImageUpload != null)
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(entityViewModel.ImageUpload.InputStream, true, true))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);
                        entity.Image = base64String;
                    }
                }
            }
            Context.Set<News>().Add(entity);
            Context.SaveChanges();
            return RedirectToAction("Index", new { entity.ClassYear });
        }

        public override ActionResult Edit(int id)
        {
            var entity = Context.Set<News>().Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var entityViewModel = new NewsViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                IsDeleted = entity.IsDeleted
            };
            if (entity.Image != null)
            {
                byte[] imageBytes = Convert.FromBase64String(entity.Image);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                entityViewModel.Image = image;
            }
            return View(entityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNews(NewsViewModel entityViewModel)
        {
            if (!ModelState.IsValid) return View("Edit", entityViewModel);
            var entity = Context.Set<News>().Find(entityViewModel.Id);

            Debug.Assert(entity != null, nameof(entity) + " != null");
            entity.Title = entityViewModel.Title;
            entity.Description = entityViewModel.Description;
            entity.IsDeleted = entityViewModel.IsDeleted;

            if (entityViewModel.ImageUpload != null)
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(entityViewModel.ImageUpload.InputStream, true, true))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);
                        entity.Image = base64String;
                    }
                }
            }
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return RedirectToAction("Index", new { entity.ClassYear });
        }
    }
}
