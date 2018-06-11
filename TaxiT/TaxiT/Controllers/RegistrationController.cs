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
            return Korisnici.korisnici;
        }
        // POST: api/Registration
        public void Post([FromBody]Korisnik k)
        {
            k.Id = (Korisnici.korisnici.Count + 1).ToString();
            k.Uloga = Enums.Uloga.Mušterija;
            Korisnici.korisnici.Add(k.Id, k);
            AddToFile(k);
            
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
