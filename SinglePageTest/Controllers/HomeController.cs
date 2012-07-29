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
            return SinglePageView("Index", "Index", GetIndexData());
        }

        public IndexModel GetIndexData()
        {
            return new IndexModel
            {
                module = Utils.GetSinglePageModuleName("Home", "Index"),
                items = new IndexItemModel[]
                {
                    new IndexItemModel { name = "One" },
                    new IndexItemModel { name = "Two" },
                }
            };
        }


        public ActionResult About()
        {
            return SinglePageView("About", "About", GetAboutData());
        }

        public AboutModel GetAboutData()
        {
            return new AboutModel
            {
                module = Utils.GetSinglePageModuleName("Home", "About"),
                message = "This is about screen. Time is " + DateTime.Now
            };
        }


        public ActionResult Contact()
        {
            return SinglePageView("Contact", "Contact", GetContactData());
        }

        public ContactModel GetContactData()
        {
            var html = new HtmlHelper(
                    new ViewContext(ControllerContext, new WebFormView(this.ControllerContext, "fake"), new ViewDataDictionary(), new TempDataDictionary(), new StringWriter()),
                    new ViewPage());
            return new ContactModel
            {
                module = Utils.GetSinglePageModuleName("Home", "Contact"),
                message = "(403) 123 4567 - Mikalai Silivonik",
                indexLink = html.SinglePageActionLink("Single Page Index", "Index")
            };
        }


        public ActionResult Tasks()
        {
            return SinglePageData(new
            {
                items = new [] 
                {
                    new { name = "Do this" },
                    new { name = "Do that" },
                    new { name = "Do nothing then" },
                }
            });
        }
    }
}
