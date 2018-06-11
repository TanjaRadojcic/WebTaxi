using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class RegistrationController : ApiController
    {
        
      
        public Dictionary<string,Korisnik> Get()
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as Dictionary<string, Korisnik>;
            if (korisnici == null)
            {
                korisnici = new Dictionary<string, Korisnik>();
            }
            return korisnici;
        }
        // POST: api/Registration
        public bool Post([FromBody]Korisnik k)
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as Dictionary<string, Korisnik>;
            bool postoji = false;
            if(korisnici== null)
            {
                korisnici = new Dictionary<string, Korisnik>();
            }
            foreach (Korisnik korisnik in korisnici.Values)
            {
                if (k.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                k.Id = (korisnici.Count + 1).ToString();
                k.Uloga = Enums.Uloga.Mušterija;
                korisnici.Add(k.Id, k);
                HttpContext.Current.Application["korisnici"] = korisnici;
                AddToFile(k);
                return true;
            }
            else
            {
                return false;
            }
            
            
        }

        [NonAction]
        public void AddToFile(Korisnik k)
        {
            FileStream stream = new FileStream(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/korisnici.txt", FileMode.Append);
            using (StreamWriter outputFile = new StreamWriter(stream))
            {
                string korisnik = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga;
                outputFile.WriteLine(korisnik);
            }
            stream.Close();
        }

        
    }
}
