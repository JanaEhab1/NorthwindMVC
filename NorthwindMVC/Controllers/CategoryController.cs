using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
    public class CategoryController : Controller
    {
        private NorthwindEntities1 db = new NorthwindEntities1();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

      
        [HttpGet]
        public ActionResult Details(int id)
        {
            var cat = db.Categories.Find(id);
            if (cat == null)
                return HttpNotFound();

            return View("Details", cat); 
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create", new Category());
        }

        [HttpPost]
        public JsonResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return Json(new { success = true, data = category });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            return Json(new { success = false, errors });
        }

        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cat = db.Categories.Find(id);
            if (cat == null)
                return HttpNotFound();

            return View("Edit", cat);
        }

        [HttpPost]
        public JsonResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, data = category });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            return Json(new { success = false, errors });
        }

      
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null) return HttpNotFound();
            return View(category);  
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int CategoryID)
        {
            var cat = db.Categories.Find(CategoryID);

            if (cat == null)
            {
                return Json(new { success = false, message = "Category not found" });
            }

        
            if (db.Products.Any(p => p.CategoryID == CategoryID))
            {
                return Json(new { success = false, message = "This category has related products and cannot be deleted." });
            }

            db.Categories.Remove(cat);
            db.SaveChanges();

            return Json(new { success = true, id = CategoryID });
        }



    }
}