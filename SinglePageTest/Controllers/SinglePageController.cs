using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinglePageTest.Extensions;

namespace SinglePageTest.Controllers
{

    public class SinglePageBindingAttribute : ActionFilterAttribute
    {
        public string PageTitle { get; set; }
        public SinglePageBindingType BindingType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = context.ActionDescriptor.Execute(context.Controller.ControllerContext, context.ActionParameters);

                if (this.BindingType == SinglePageBindingType.Server)
                {
                    result = result.GetType().IsAnonymousType() ? result.ToExpando() : result;

                    context.HttpContext.Response.Headers.Add("page-title", this.GetSafePageTitle(context.ActionDescriptor));

                    context.Controller.ViewData.Model = result;
                    context.Result = new PartialViewResult
                    {
                        ViewName = context.ActionDescriptor.ActionName,
                        ViewData = context.Controller.ViewData,
                        TempData = context.Controller.TempData
                    };
                }
                else
                {
                    context.HttpContext.Response.Headers.Add("page-title", this.GetSafePageTitle(context.ActionDescriptor));
                    context.HttpContext.Response.Headers.Add("requires-template", "true");
                    context.Result = new JsonResult
                    {
                        Data = result,
                        JsonRequestBehavior = JsonRequestBehavior.DenyGet
                    };
                }
            }
            else
            {
                if (this.BindingType == SinglePageBindingType.Server)
                {
                    SetupTitleAndModule(context.Controller, context.ActionDescriptor);

                    var result = context.ActionDescriptor.Execute(context.Controller.ControllerContext, context.ActionParameters);
                    result = result.GetType().IsAnonymousType() ? result.ToExpando() : result;

                    context.Controller.ViewData.Model = result;
                    context.Result = new ViewResult
                    {
                        ViewName = context.ActionDescriptor.ActionName,
                        MasterName = "~/Views/_Single.cshtml",
                        ViewData = context.Controller.ViewData,
                        TempData = context.Controller.TempData
                    };
                }
                else
                {
                    SetupTitleAndModule(context.Controller, context.ActionDescriptor);

                    context.Controller.ViewBag.ClientBinding = true;

                    context.Result = new ViewResult
                    {
                        ViewName = "~/Views/_Single.cshtml",
                        ViewData = context.Controller.ViewData,
                        TempData = context.Controller.TempData
                    };
                }
            }
        }

        private void SetupTitleAndModule(ControllerBase controller, ActionDescriptor actionDescriptor)
        {
            var viewBag = controller.ViewBag;
            viewBag.Title = this.GetSafePageTitle(actionDescriptor);
            viewBag.Module = Utils.GetSinglePageModuleName(actionDescriptor.ControllerDescriptor.ControllerName, actionDescriptor.ActionName);
        }

        private string GetSafePageTitle(ActionDescriptor actionDescriptor)
        {
            return !string.IsNullOrWhiteSpace(this.PageTitle) ? this.PageTitle : actionDescriptor.ActionName;
        }
    }

    public enum SinglePageBindingType
    {
        Server, Client
    }
}
