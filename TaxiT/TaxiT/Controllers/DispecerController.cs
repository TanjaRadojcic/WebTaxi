using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class DispecerController : ApiController
    {
        // GET: api/Dispecer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Dispecer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dispecer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dispecer/5
        public bool Put(int id, [FromBody]Dispecer k)
        {
            bool postoji = false;
            if (Dispeceri.dispeceri == null)
            {
                Dispeceri.dispeceri = new Dictionary<int, Dispecer>();
            }
            foreach (Dispecer dispecer in Dispeceri.dispeceri.Values)
            {
                if (k.KorisnickoIme == dispecer.KorisnickoIme && k.Id != dispecer.Id)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                
                Dispeceri.dispeceri[id] = k;
                ChangeToFile(k);

                return true;
            }
            else
            {
                return false;
            }
        }

        // DELETE: api/Dispecer/5
        public void Delete(int id)
        {
        }

        [NonAction]
        public void ChangeToFile(Dispecer k)
        {
            
                var file = File.ReadAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/dispeceri.txt");
                file[k.Id] = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga;
                File.WriteAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/dispeceri.txt", file);
            
        }
    }
}
