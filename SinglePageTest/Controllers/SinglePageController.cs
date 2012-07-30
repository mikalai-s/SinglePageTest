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
        protected ActionResult ServerBindingResult<T>(string pageTitle, Func<T> dataResolver)
        {
            string controller = this.Request.RequestContext.RouteData.Values["controller"].ToString();
            string action = this.Request.RequestContext.RouteData.Values["action"].ToString();

            // in case of SinglePage navigation to this action we return just partial view
            if (Request.IsAjaxRequest())
            {
                this.Response.Headers.Add("page-title", pageTitle);
                return PartialView(action, dataResolver());
            }

            ViewBag.Title = pageTitle;
            ViewBag.Module = Utils.GetSinglePageModuleName(controller, action);
            return View(action, "~/Views/_Single.cshtml", dataResolver());
        }

        /// <summary>
        /// Client side binding occurs on client. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageTitle"></param>
        /// <param name="dataResolver"></param>
        /// <returns></returns>
        protected ActionResult ClientBindingResult<T>(string pageTitle, Func<T> dataResolver)
        {
            if (Request.IsAjaxRequest())
            {
                this.Response.Headers.Add("page-title", pageTitle);
                this.Response.Headers.Add("requires-template", "true");
                return Json(dataResolver(), JsonRequestBehavior.DenyGet);
            }

            string controller = this.Request.RequestContext.RouteData.Values["controller"].ToString();
            string action = this.Request.RequestContext.RouteData.Values["action"].ToString();

            ViewBag.Title = pageTitle;
            ViewBag.Module = Utils.GetSinglePageModuleName(controller, action);
            return View("~/Views/_ClientBindingView.cshtml");
        }
    }
}
