using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class RegistrationVozacController : ApiController
    {
        // GET: api/RegistrationVozac
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RegistrationVozac/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RegistrationVozac
        public bool Post([FromBody]Vozac v)
        {
            bool postoji = false;
            if (Vozaci.vozaci == null)
            {
                Vozaci.vozaci = new Dictionary<int, Vozac>();
            }
            foreach (Vozac vozac in Vozaci.vozaci.Values)
            {
                if (v.KorisnickoIme == vozac.KorisnickoIme)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                v.Id = Vozaci.vozaci.Count;
                v.Uloga = Enums.Uloga.Vozač;
                Vozaci.vozaci.Add(v.Id, v);

               // AddToFile(k);
                return true;
            }
            else
            {
                return false;
            }


        }

        // PUT: api/RegistrationVozac/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RegistrationVozac/5
        public void Delete(int id)
        {
        }
    }
}
