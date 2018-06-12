using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class FindController : ApiController
    {
        // GET: api/Find
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Find/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Find
        public Korisnik Post([FromBody]string value)
        {
            Korisnik k;

            foreach(var korisnik in Korisnici.korisnici.Values)
            {
                if(korisnik.KorisnickoIme == value)
                {
                    k = korisnik;
                    return k;
                }
            }
            foreach (var dispecer in Dispeceri.dispeceri.Values)
            {
                if (dispecer.KorisnickoIme == value)
                {
                    k = dispecer;
                    return k;
                }
            }
            foreach (var vozac in Vozaci.vozaci.Values)
            {
                if (vozac.KorisnickoIme == value)
                {
                    k = vozac;
                    return k;
                }
            }

            
            return null;
        }

        // PUT: api/Find/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Find/5
        public void Delete(int id)
        {
        }
    }
}
