using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinglePageTest.Models
{
    public struct IndexModel
    {
        public string title;
        public string module;
        public IEnumerable<IndexItemModel> items;
    }

    public struct IndexItemModel
    {
        public string name;
    }

    public struct AboutModel
    {
        public string title;
        public string module;
        public string message;
    }

    public struct ContactModel
    {
        public string title;
        public string module;
        public string message;
        public object indexLink;
    }
}