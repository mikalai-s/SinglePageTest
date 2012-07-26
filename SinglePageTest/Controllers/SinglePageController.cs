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
        protected PartialViewResult PartialView(string viewName, IPageModel model)
        {
            this.Response.Headers.Add("page-title", model.title);
            return base.PartialView(viewName, model);
        }
    }
}
