using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SinglePageTest.Controllers
{
    public class HomeApiController : ApiController
    {
        // GET api/homeapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/homeapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/homeapi
        public void Post(string value)
        {
        }

        // PUT api/homeapi/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/homeapi/5
        public void Delete(int id)
        {
        }
    }
}
