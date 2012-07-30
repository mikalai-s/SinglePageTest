using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SinglePageTest.Extensions;
using SinglePageTest.Models;

namespace SinglePageTest.Controllers
{
    public class HomeController : SinglePageController
    {
        public ActionResult Index()
        {
            return ServerBindingResult("Index", 
                () => new IndexModel
                {
                    items = new IndexItemModel[]
                    {
                        new IndexItemModel { name = "One" },
                        new IndexItemModel { name = "Two" },
                    }
                }
            );
        }


        public ActionResult About()
        {
            return ServerBindingResult("About", 
                () => new AboutModel
                {
                    message = "This is about screen. Time is " + DateTime.Now
                }
            );
        }


        public ActionResult Contact()
        {
            var html = new HtmlHelper(
                    new ViewContext(ControllerContext, new WebFormView(this.ControllerContext, "fake"), new ViewDataDictionary(), new TempDataDictionary(), new StringWriter()),
                    new ViewPage());

            return ServerBindingResult("Contact", 
                () => new ContactModel
                {
                    message = "(403) 123 4567 - Mikalai Silivonik",
                    indexLink = html.SinglePageActionLink("Single Page Index", "Index")
                }
            );
        }


        public ActionResult Tasks()
        {
            return ClientBindingResult("Tasks",
                () => new
                {
                    items = new[]
                    {
                        new { name = "Do this" },
                        new { name = "Do that" },
                        new { name = "Do nothing then" },
                    }
                }
            );
        }
    }
}
