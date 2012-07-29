using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinglePageTest.Models;

namespace SinglePageTest.Controllers
{
    public class SinglePageController : Controller
    {
        protected ActionResult SinglePageView(string viewName, string pageTitle, object model)
        {
            // in case of SinglePage navigation to this action we return just partial view
            if (Request.IsAjaxRequest())
            {
                this.Response.Headers.Add("page-title", pageTitle);
                return PartialView(viewName, model);
            }

            ViewBag.Title = pageTitle;
            return View(viewName, "~/Views/_Single.cshtml", model);
        }

        protected JsonResult SinglePageData(object data)
        {
            this.Response.Headers.Add("requires-template", "true");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
