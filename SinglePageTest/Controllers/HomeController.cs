using System;
using System.Collections.Generic;
using System.Dynamic;
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
            if(Request.IsAjaxRequest())
                return PartialView("Index", GetIndexData());
            return View("Index", "~/Views/_Single.cshtml", GetIndexData());
        }

        public IndexModel GetIndexData()
        {
            return new IndexModel
            {
                title = "Index",
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
            if(Request.IsAjaxRequest())
                return PartialView("About", GetAboutData());
            return View("About", "~/Views/_Single.cshtml", GetAboutData());
        }

        public AboutModel GetAboutData()
        {
            return new AboutModel
            {
                title = "About",
                module = Utils.GetSinglePageModuleName("Home", "About"),
                message = "This is about screen. Time is " + DateTime.Now
            };
        }


        public ActionResult Contact()
        {
            if(Request.IsAjaxRequest())
                return PartialView("Contact", GetContactData());
            return View("Contact", "~/Views/_Single.cshtml", GetContactData());
        }

        public ContactModel GetContactData()
        {
            return new ContactModel
            {
                title = "Contact",
                module = Utils.GetSinglePageModuleName("Home", "Contact"),
                message = "(403) 123 4567 - Mikalai Silivonik",
                indexLink = MyMvcHtmlString.Create(this.Html.SinglePageActionLink("Single Page Index", "Index").ToHtmlString())
            };
        }
    }
}
