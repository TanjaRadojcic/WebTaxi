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
    public class KorisnikController : ApiController
    {
        // GET: api/Korisnik
        public Dictionary<int, Korisnik> Get()
        {

            if (Korisnici.korisnici == null)
            {
                Korisnici.korisnici = new Dictionary<int, Korisnik>();
            }
            return Korisnici.korisnici;
        }

        // GET: api/Korisnik/5
        public string Get(int id)
        {
            return "value";
        }

        //registracija
        // POST: api/Korisnik
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

        //izmena
        // PUT: api/Korisnik/5
        public bool Put(int id, [FromBody]Korisnik k)
        {
            bool postoji = false;
            if (Korisnici.korisnici == null)
            {
                Korisnici.korisnici = new Dictionary<int, Korisnik>();
            }
            foreach (Korisnik korisnik in Korisnici.korisnici.Values)
            {
                if (k.KorisnickoIme == korisnik.KorisnickoIme && id != korisnik.Id)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                Korisnici.korisnici[id] = k;
                ChangeToFile(k);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        // DELETE: api/Korisnik/5
        public void Delete(int id)
        {
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

        [NonAction]
        public void ChangeToFile(Korisnik k)
        {

            var file = File.ReadAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/korisnici.txt");
            file[k.Id] = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga;
            File.WriteAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/korisnici.txt", file);

        }
    }
}
