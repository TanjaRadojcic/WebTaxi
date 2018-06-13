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
            if (k.Uloga == Enums.Uloga.Mušterija)
            {
                bool postoji = false;
                if (Korisnici.korisnici == null)
                {
                    Korisnici.korisnici = new Dictionary<int, Korisnik>();
                }
                foreach (Korisnik korisnik in Korisnici.korisnici.Values)
                {
                    if (k.KorisnickoIme == korisnik.KorisnickoIme && k.Id != korisnik.Id)
                    {
                        postoji = true;
                        break;
                    }
                }
                if (!postoji)
                {
                    Korisnici.korisnici[k.Id] = k;
                    AddToFile(k);


                    // AddToFile(k);
                    return true;
                }
                else
                {
                    return false;
                }
            }else if(k.Uloga == Enums.Uloga.Dispečer)
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
                    Dispecer d = new Dispecer(k.Id,k.KorisnickoIme,k.Lozinka,k.Ime,k.Prezime,k.Pol,k.JMBG,k.Kontakt,k.Email,k.Uloga);
                    
                    Dispeceri.dispeceri[k.Id] = d;
                    AddToFile(k);
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;


        }

        [NonAction]
        public void AddToFile(Korisnik k)
        {
            if (k.Uloga == Enums.Uloga.Mušterija)
            {
                var file = File.ReadAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/korisnici.txt");
                file[k.Id] = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga;
                File.WriteAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/korisnici.txt", file);
            }
            else if (k.Uloga == Enums.Uloga.Dispečer)
            {
                var file = File.ReadAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/dispeceri.txt");
                file[k.Id] = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga;
                File.WriteAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/dispeceri.txt", file);
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
