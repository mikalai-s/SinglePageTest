using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinglePageTest.Models
{
    public struct IndexModel
    {
        public string module { get; set; }
        public IEnumerable<IndexItemModel> items { get; set; }
    }

    public struct IndexItemModel
    {
        public string name { get; set; }
    }


    public struct AboutModel
    {
        public string module { get; set; }
        public string message { get; set; }
    }


    public struct ContactModel
    {
        public string module { get; set; }
        public string message { get; set; }
        public object indexLink { get; set; }
    }
}