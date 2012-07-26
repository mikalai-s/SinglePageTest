using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Routing;

namespace SinglePageTest.Extensions
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
            {
                object value = null;

                var ie = item.Value as IEnumerable;
                if (ie != null)
                {
                    value = ie.ToExpandoList();
                }
                else
                {
                    value = item.Value.GetType().IsAnonymousType() ? item.Value.ToExpando() : item.Value;
                }
                expando.Add(new KeyValuePair<string, object>(item.Key, value));
            }
            return (ExpandoObject)expando;
        }

        public static IEnumerable<ExpandoObject> ToExpandoList(this IEnumerable ie)
        {
            foreach(var item in ie)
                yield return item.ToExpando();
        }

        public static bool IsAnonymousType(this Type type)
        {
            bool hasCompilerGeneratedAttribute = true;// type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0;
            bool nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            bool isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;
            return isAnonymousType;            
        }
    }
}