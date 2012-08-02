using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SinglePageTest.Extensions;

namespace SinglePageTest.Controllers
{
    public class HomeController : Controller
    {
        [SinglePageBinding]
        public object Index()
        {
            return new 
            {
                items = new []
                {
                    new { name = "One" },
                    new { name = "Two" },
                }
            };
        }

        [SinglePageBinding(PageTitle = "About Project")]
        public object About()
        {
            return new
            {
                message = "This is about screen. Time is " + DateTime.Now
            };
        }

        [SinglePageBinding]
        public object Contact()
        {
            var html = new HtmlHelper(
                    new ViewContext(ControllerContext, new WebFormView(this.ControllerContext, "fake"), new ViewDataDictionary(), new TempDataDictionary(), new StringWriter()),
                    new ViewPage());

            return new
            {
                message = "(403) 123 4567 - Mikalai Silivonik",
                indexLink = html.SinglePageActionLink("Single Page Index", "Index")
            };
        }


        [SinglePageBinding(BindingType = SinglePageBindingType.Client)]
        public object Tasks()
        {
            return new
            {
                items = new[]
                {
                    new { name = "Do this" },
                    new { name = "Do that" },
                    new { name = "Do nothing then" },
                }
            };
        }
    }
}
