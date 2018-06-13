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
        
      
        public Dictionary<int,Korisnik> Get()
        {
            
            if (Korisnici.korisnici == null)
            {
                Korisnici.korisnici = new Dictionary<int, Korisnik>();
            }
            return Korisnici.korisnici;
        }
        // POST: api/Registration
        public bool Post([FromBody]Korisnik k)
        {
            
            bool postoji = false;
            if(Korisnici.korisnici == null)
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
                k.Id = Korisnici.korisnici.Count;
                k.Uloga = Enums.Uloga.Mušterija;
                Korisnici.korisnici.Add(k.Id, k);
                
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
