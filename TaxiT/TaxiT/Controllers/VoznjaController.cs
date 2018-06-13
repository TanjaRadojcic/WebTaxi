using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class VoznjaController : ApiController
    {
        // GET: api/Voznja
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Voznja/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Voznja
        public bool Post([FromBody]Voznja voznja)
        {
            if(Voznje.voznje == null)
            {
                Voznje.voznje = new Dictionary<int, Voznja>();
            }
            return false;
        }

        // PUT: api/Voznja/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Voznja/5
        public void Delete(int id)
        {
        }
    }
}
