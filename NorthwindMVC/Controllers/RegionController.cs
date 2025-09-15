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
        public JsonResult UpdateRegion(int RegionId , string RegionDescription)
        {
            var region = db.Regions.FirstOrDefault(r => r.RegionID == RegionId);
            if (region != null)
            {
                region.RegionDescription = RegionDescription;
                try
                {
                    db.SaveChanges();
                    return Json(new { success = true, message = "Region updated successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                };
            }
            else
            {
                return Json(new { success = false, message = "Region not found" });
            }

        }
    }
}