using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinglePageTest.Extensions
{
    public static class Utils
    {
        public static string GetSinglePageModuleName(string controller, string action)
        {
            return string.Format("views/{0}-{1}", controller.ToLower(), action.ToLower());
        }
    }
}