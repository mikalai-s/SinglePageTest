using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinglePageTest.Models
{
    public interface IPageModel
    {
        string title { get; }
    }


    public struct IndexModel : IPageModel
    {
        public string title { get; set; }
        public string module { get; set; }
        public IEnumerable<IndexItemModel> items { get; set; }
    }

    public struct IndexItemModel
    {
        public string name { get; set; }
    }


    public struct AboutModel : IPageModel
    {
        public string title { get; set; }
        public string module { get; set; }
        public string message { get; set; }
    }


    public struct ContactModel : IPageModel
    {
        public string title { get; set; }
        public string module { get; set; }
        public string message { get; set; }
        public object indexLink { get; set; }
    }
}