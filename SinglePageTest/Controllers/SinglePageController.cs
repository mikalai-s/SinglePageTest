using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinglePageTest.Extensions;
using SinglePageTest.Models;

namespace SinglePageTest.Controllers
{
    public class SinglePageController : Controller
    {
        protected ActionResult ServerBindingResult(string pageTitle, object model)
        {
            string controller = this.Request.RequestContext.RouteData.Values["controller"].ToString();
            string action = this.Request.RequestContext.RouteData.Values["action"].ToString();

            // in case of SinglePage navigation to this action we return just partial view
            if (Request.IsAjaxRequest())
            {
                this.Response.Headers.Add("page-title", pageTitle);
                return PartialView(action, model);
            }

            ViewBag.Title = pageTitle;
            return View(action, "~/Views/_Single.cshtml", model);
        }

        protected ActionResult ClientBindingResult(string pageTitle, Func<object> dataResolver)
        {
            if (Request.IsAjaxRequest())
            {
                this.Response.Headers.Add("page-title", pageTitle);
                this.Response.Headers.Add("requires-template", "true");
                return Json(dataResolver(), JsonRequestBehavior.DenyGet);
            }

            string controller = this.Request.RequestContext.RouteData.Values["controller"].ToString();
            string action = this.Request.RequestContext.RouteData.Values["action"].ToString();

            var module = Utils.GetSinglePageModuleName(controller, action);
            return View("~/Views/_ClientBindingView.cshtml", "~/Views/_Single.cshtml", module);
        }
    }
}
