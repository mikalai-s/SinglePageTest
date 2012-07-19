using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinglePageTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    title = "Index",
                    items = new[]
                    {
                        new { name = "One" },
                        new { name = "Two" },
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("~/Views/Shared/_Single.cshtml", (object)"views/home-index");
            }
        }

        public ActionResult About()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    title = "About",
                    message = "This is about screen. TOday is " + DateTime.Now
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("~/Views/Shared/_Single.cshtml", (object)"views/home-about");
            }
        }

        public ActionResult Contact()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    title = "Contact",
                    message = "403 123 4567 - Mikalai Silivonik"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("~/Views/Shared/_Single.cshtml", (object)"views/home-contact");
            }
        }
    }
}
