using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinglePageTest.Controllers
{
    public abstract class SinglePageController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.IsAjaxRequest())
            {
                base.OnActionExecuting(filterContext);
            }
            var module = string.Format("views/{0}-{1}",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower(),
                filterContext.ActionDescriptor.ActionName.ToLower());
            filterContext.Result = View("~/Views/_Single.cshtml", module);
        }
    }

}
