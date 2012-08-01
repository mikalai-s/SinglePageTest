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
    internal static class ObjectExtensions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
            {
                object value = null;

                var ie = item.Value as IEnumerable;
                if (ie != null && ie.GetType().Name.Contains("AnonymousType"))
                {
                    var list = new List<ExpandoObject>();
                    foreach (var i in ie)
                        list.Add(i.ToExpando());
                    value = list;
                }
                else
                {
                    value = item.Value;
                }

                expando.Add(new KeyValuePair<string, object>(item.Key, value));
            }
            return (ExpandoObject)expando;
        }

        public static bool IsAnonymousType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (((!Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false) || !type.IsGenericType) || !type.Name.Contains("AnonymousType")) || (!type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) && !type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            TypeAttributes attributes = type.Attributes;
            return (0 == 0);
        }
    }
}