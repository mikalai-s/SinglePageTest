using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SinglePageTest.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary());
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, null, routeValues, new RouteValueDictionary());
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, null, routeValues, htmlAttributes);
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException("", "linkText");
            }
            return MvcHtmlString.Create(GenerateSinglePageLink(htmlHelper, htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, routeValues, htmlAttributes));
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            return htmlHelper.SinglePageActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString SinglePageActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException("", "linkText");
            }
            return MvcHtmlString.Create(GenerateSinglePageLink(htmlHelper, htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes));
        }

        public static string GenerateSinglePageLink(HtmlHelper htmlHelper, RequestContext requestContext, System.Web.Routing.RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return GenerateSinglePageLink(htmlHelper, requestContext, routeCollection, linkText, routeName, actionName, controllerName, null, null, null, routeValues, htmlAttributes);
        }

        public static string GenerateSinglePageLink(HtmlHelper htmlHelper, RequestContext requestContext, System.Web.Routing.RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return GenerateSinglePageLinkInternal(htmlHelper, requestContext, routeCollection, linkText, routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, true);
        }

        private static string GenerateSinglePageLinkInternal(HtmlHelper htmlHelper, RequestContext requestContext, System.Web.Routing.RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool includeImplicitMvcValues)
        {
            string str = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
            TagBuilder builder2 = new TagBuilder("a")
            {
                InnerHtml = !string.IsNullOrEmpty(linkText) ? HttpUtility.HtmlEncode(linkText) : string.Empty
            };
            TagBuilder builder = builder2;
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("href", str);

            controllerName = string.IsNullOrEmpty(controllerName) ? new ReflectedControllerDescriptor(htmlHelper.ViewContext.Controller.GetType()).ControllerName : controllerName;
            builder.MergeAttribute("module", Utils.GetSinglePageModuleName(controllerName, actionName));
            return builder.ToString(TagRenderMode.Normal);
        }
    }
}