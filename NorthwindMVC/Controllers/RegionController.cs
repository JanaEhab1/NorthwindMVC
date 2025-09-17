using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using NorthwindMVC.Models;

namespace NorthwindMVC.Controllers
{
    public class RegionController : Controller
    {
        private NORTHWINDEntities db = new NORTHWINDEntities();
        public RegionController()
        {
            // Disable EF proxy objects to avoid circular reference in JSON
            db.Configuration.ProxyCreationEnabled = false;
        }
        public ActionResult Index()
        {
            var regions = db.Regions.ToList();
            return View(regions);
        }

        [HttpGet]
        public JsonResult GetRegion(int id)
        {
            var region = db.Regions.FirstOrDefault(r => r.RegionID == id);
            if (region != null)
            {
                return Json(new { success = true, data = region }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Region not found" }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public JsonResult UpdateRegion(int RegionId, string RegionDescription)
        {
            if (string.IsNullOrWhiteSpace(RegionDescription))
            {
                return Json(new { success = false, message = "Region Description cannot be empty" });
            }

            var region = db.Regions.FirstOrDefault(r => r.RegionID == RegionId);
            if (region != null)
            {
                string newDescription = RegionDescription.Trim();
                string currentDescription = region.RegionDescription.Trim();

                if (currentDescription.Equals(newDescription, StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new { success = false, message = "No changes detected" });
                }

                bool exists = db.Regions.Any(r =>
                    r.RegionID != RegionId &&
                    r.RegionDescription.Trim().ToLower() == newDescription.ToLower());

                if (exists)
                {
                    return Json(new { success = false, message = "A region with this name already exists." });
                }

                region.RegionDescription = newDescription;
                try
                {
                    db.SaveChanges();
                    return Json(new { success = true, message = "Region updated successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Region not found" });
            }
        }




        [HttpPost]
        public JsonResult AddRegion(string RegionDescription)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RegionDescription))
                {
                    return Json(new { success = false, message = "Description is required" });
                }

                string trimmedDescription = RegionDescription.Trim();

                bool exists = db.Regions.Any(r => r.RegionDescription.Trim().ToLower() == trimmedDescription.ToLower());
                if (exists)
                {
                    return Json(new { success = false, message = "A region with this name already exists." });
                }

                var newRegion = new Region
                {
                    RegionDescription = trimmedDescription
                };

                db.Regions.Add(newRegion);
                db.SaveChanges();

                return Json(new { success = true, message = "Region added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }




        [HttpPost]
        public JsonResult DeleteRegion(int id)
        {
            try
            {
                var region = db.Regions.FirstOrDefault(r => r.RegionID == id);
                if (region == null)
                {
                    return Json(new { success = false, message = "Region not found." });
                }

                bool hasTerritories = db.Territories.Any(t => t.RegionID == id);
                if (hasTerritories)
                {
                    return Json(new { success = false, message = "You cannot delete this region because it has related territories." });
                }

                db.Regions.Remove(region);
                db.SaveChanges();

                return Json(new { success = true, message = "Region deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }





    }
}