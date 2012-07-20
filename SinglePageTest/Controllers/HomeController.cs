using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinglePageTest.Controllers
{
    public class HomeController : SinglePageController
    {
        public ActionResult Index()
        {
            return Json(new
            {
                title = "Index",
                items = new[]
                {
                    new { name = "One" },
                    new { name = "Two" },
                }
            });
        }

        public ActionResult About()
        {
            return Json(new
            {
                title = "About",
                message = "This is about screen. Today is " + DateTime.Now
            });
        }

        public ActionResult Contact()
        {
            return Json(new
            {
                title = "Contact",
                message = "(403) 123 4567 - Mikalai Silivonik"
            });
        }
    }
}
