using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinglePageTest.Extensions
{
    public class MyMvcHtmlString : HtmlString
    {
        private readonly string _value;
        public static readonly MvcHtmlString Empty = Create(string.Empty);

        public MyMvcHtmlString(string value)
            : base(value ?? string.Empty)
        {
            this._value = value ?? string.Empty;
        }        
        
        public static MvcHtmlString Create(string value)
        {
            return new MvcHtmlString(value);
        }

        public static bool IsNullOrEmpty(MyMvcHtmlString value)
        {
            if (value != null)
            {
                return (value._value.Length == 0);
            }
            return true;
        }

        public override string ToString()
        {
            return this.ToHtmlString();
        }
    }
}