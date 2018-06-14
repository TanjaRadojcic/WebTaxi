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
    public class VozacController : ApiController
    {
        // GET: api/Vozac
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Vozac/5
        public string Get(int id)
        {
            return "value";
        }

        //registracija
        // POST: api/Vozac
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
                v.Automobil.Id = Automobili.automobili.Count;
                v.Automobil.Vozac = v.KorisnickoIme;
                v.Lokacija.Id = Lokacije.lokacije.Count;
                Vozaci.vozaci.Add(v.Id, v);

                AddToFileVozac(v);
                AddToFileAutomobil(v.Automobil);
                AddToFileLokacija(v.Lokacija);
                AddToFileAdresa(v.Lokacija.Adresa);

                return true;
            }
            else
            {
                return false;
            }


        }

        [NonAction]
        public void AddToFileVozac(Vozac v)
        {
            FileStream stream = new FileStream(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/vozaci.txt", FileMode.Append);
            using (StreamWriter outputFile = new StreamWriter(stream))
            {
                string vozac = v.Id + ";" + v.KorisnickoIme + ";" + v.Lozinka + ";" + v.Ime + ";" + v.Prezime + ";" + v.Pol + ";" + v.JMBG + ";" + v.Kontakt + ";" + v.Email
                    + ";" + v.Uloga + ";" + v.Lokacija.Id + ";" + v.Lokacija.X + ";" + v.Lokacija.Y + ";" + v.Lokacija.Adresa.Id + ";" + v.Lokacija.Adresa.Ulica + ";" + v.Lokacija.Adresa.Broj + ";"
                    + v.Lokacija.Adresa.Mesto + ";" + v.Lokacija.Adresa.Zip + ";" + v.Automobil.Id + ";" + v.Automobil.Vozac + ";" + v.Automobil.Godiste + ";" + v.Automobil.Registracija + ";" 
                    + v.Automobil.BrojVozila + ";" + v.Automobil.TipAutomobila +";"+v.Zauzet;
                 outputFile.WriteLine(vozac);
            }
            stream.Close();
        }

        [NonAction]
        public void AddToFileAutomobil(Automobil v)
        {
            FileStream stream = new FileStream(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/automobili.txt", FileMode.Append);
            using (StreamWriter outputFile = new StreamWriter(stream))
            {
                string automobil = v.Id + ";" + v.Vozac + ";" + v.Godiste + ";" + v.Registracija + ";" + v.BrojVozila + ";" + v.TipAutomobila;
                outputFile.WriteLine(automobil);
            }
            stream.Close();
        }
        [NonAction]
        public void AddToFileLokacija(Lokacija v)
        {
            FileStream stream = new FileStream(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/lokacije.txt", FileMode.Append);
            using (StreamWriter outputFile = new StreamWriter(stream))
            {
                string lokacija = v.Id + ";" + v.X + ";" + v.Y + ";" + v.Adresa.Id;
                outputFile.WriteLine(lokacija);
            }
            stream.Close();
        }

        [NonAction]
        public void AddToFileAdresa(Adresa v)
        {
            FileStream stream = new FileStream(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/adrese.txt", FileMode.Append);
            using (StreamWriter outputFile = new StreamWriter(stream))
            {
                string adresa = v.Id + ";" + v.Ulica + ";" + v.Broj + ";" + v.Mesto + ";" + v.Zip;
                outputFile.WriteLine(adresa);
            }
            stream.Close();
        }

        //izmena
        // PUT: api/Vozac/5
        public void Put(int id, [FromBody]Vozac v)
        {
        }

        // DELETE: api/Vozac/5
        public void Delete(int id)
        {
        }
    }
}
