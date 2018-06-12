using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class IzmeniController : ApiController
    {
        // GET: api/Izmeni
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Izmeni/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Izmeni
        public bool Post([FromBody]Korisnik k)
        {

            bool postoji = false;
            if (Korisnici.korisnici == null)
            {
                Korisnici.korisnici = new Dictionary<int, Korisnik>();
            }
            foreach (Korisnik korisnik in Korisnici.korisnici.Values)
            {
                if (k.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                k.Id = Korisnici.korisnici.Count + 1;
                k.Uloga = Enums.Uloga.Mušterija;
                Korisnici.korisnici.Add(k.Id, k);

               // AddToFile(k);
                return true;
            }
            else
            {
                return false;
            }


        }

        // PUT: api/Izmeni/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Izmeni/5
        public void Delete(int id)
        {
        }
    }
}
