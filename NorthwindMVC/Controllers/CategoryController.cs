using System.Linq;
using System.Web.Mvc;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
    public class CategoryController : Controller
    {
        private NorthwindEntities1 db = new NorthwindEntities1();
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }



        [HttpPost]
        public ActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges(); 
            
            return Json(new
            {
                success = true,
                name = category.CategoryName,
                desc = category.Description
            });
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            var cat = db.Categories.Find(category.CategoryID);
            if (cat == null)
                return Json(new { success = false });

            cat.CategoryName = category.CategoryName;
            cat.Description = category.Description;
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var cat = db.Categories.Find(id);
            if (cat == null) return Json(new { success = false });

            db.Categories.Remove(cat);
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var cat = db.Categories.Find(id);
            if (cat == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                success = true,
                id = cat.CategoryID,
                name = cat.CategoryName,
                desc = cat.Description
            }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult Edit(Category category)
        //{
        //    db.Entry(category).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}






    }
}