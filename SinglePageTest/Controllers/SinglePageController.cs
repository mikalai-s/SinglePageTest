using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinglePageTest.Extensions;

namespace SinglePageTest.Controllers
{
    public abstract class SinglePageController : Controller
    {
        protected HtmlHelper Html
        {
            get
            {
                return this._html ?? (this._html = new HtmlHelper(
                    new ViewContext(ControllerContext, new WebFormView(this.ControllerContext, "fake"), new ViewDataDictionary(), new TempDataDictionary(), new StringWriter()),
                    new ViewPage()));
            }
        } private HtmlHelper _html = null;


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.IsAjaxRequest())
            {
                // if it's a view model load request
                base.OnActionExecuting(filterContext);
            }
            else if (Request.HttpMethod == "GET")
            {
                // if it's a page request
                var module = Utils.GetSinglePageModuleName(
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName);
                filterContext.Result = View("~/Views/_Single.cshtml", model: module);

                // cache single page for next 24 hours
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Now.AddHours(24));
            }
            else
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }

}
