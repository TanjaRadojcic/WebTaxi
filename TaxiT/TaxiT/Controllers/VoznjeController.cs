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
    public class VoznjeController : ApiController
    {
        // GET: api/Voznje
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Voznje/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Voznje
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Voznje/5
        public bool Put(int id, [FromBody]string value)
        {
            if (Voznje.voznje[id].Status == Enums.StatusVoznje.Kreirana)
            {
                if (value == "otkazi")
                {
                    Voznje.voznje[id].Status = Enums.StatusVoznje.Otkazana;
                    ChangeToFile(Voznje.voznje[id]);
                    return true;
                }
                else
                {
                    Voznje.voznje[id].Status = Enums.StatusVoznje.Obrađena;
                    Voznje.voznje[id].Vozac = value;
                    foreach (var v in Vozaci.vozaci.Values)
                    {
                        if (v.KorisnickoIme == Voznje.voznje[id].Vozac)
                        {
                            v.Zauzet = true;
                            ChangeToFileVozac(v);
                        }
                    }
                    ChangeToFile(Voznje.voznje[id]);
                    return true;
                }
            }
            return false;
        }

        // DELETE: api/Voznje/5
        public void Delete(int id)
        {
        }
        [NonAction]
        public void ChangeToFileVozac(Vozac v)
        {

            var file = File.ReadAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/vozaci.txt");
            file[v.Id] = v.Id + ";" + v.KorisnickoIme + ";" + v.Lozinka + ";" + v.Ime + ";" + v.Prezime + ";" + v.Pol + ";" + v.JMBG + ";" + v.Kontakt + ";" + v.Email
                   + ";" + v.Uloga + ";" + v.Lokacija.Id + ";" + v.Lokacija.X + ";" + v.Lokacija.Y + ";" + v.Lokacija.Adresa.Id + ";" + v.Lokacija.Adresa.Ulica + ";" + v.Lokacija.Adresa.Broj + ";"
                   + v.Lokacija.Adresa.Mesto + ";" + v.Lokacija.Adresa.Zip + ";" + v.Automobil.Id + ";" + v.Automobil.Vozac + ";" + v.Automobil.Godiste + ";" + v.Automobil.Registracija + ";"
                   + v.Automobil.BrojVozila + ";" + v.Automobil.TipAutomobila + ";" + v.Zauzet;

            File.WriteAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/vozaci.txt", file);

        }

        [NonAction]
        public void ChangeToFile(Voznja v)
        {

            var file = File.ReadAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/voznje.txt");
            file[v.Id] = v.Id + ";" + v.Datum.ToString() + ";" + v.PocetnaLokacija.Id + ";" + v.PocetnaLokacija.X + ";" + v.PocetnaLokacija.Y + ";" + v.PocetnaLokacija.Adresa.Id + ";" +
                    v.PocetnaLokacija.Adresa.Ulica + ";" + v.PocetnaLokacija.Adresa.Broj + ";" + v.PocetnaLokacija.Adresa.Mesto + ";" + v.PocetnaLokacija.Adresa.Zip + ";" + v.TipAutomobila + ";" +
                    v.Musterija + ";" + v.Odrediste.Id + ";" + v.Odrediste.X + ";" + v.Odrediste.Y + ";" + v.Odrediste.Adresa.Id + ";" + v.Odrediste.Adresa.Ulica + ";" + v.Odrediste.Adresa.Broj + ";"
                    + v.Odrediste.Adresa.Mesto + ";" + v.Odrediste.Adresa.Zip + ";" + v.Dispecer + ";" + v.Vozac + ";" + v.Iznos + ";" + v.Komentar.Id + ";" + v.Komentar.Opis + ";" + v.Komentar.DatumObjave.ToString()
                    + ";" + v.Komentar.Korisnik + ";" + v.Komentar.Voznja + ";" + v.Komentar.Ocena + ";" + v.Status;
            File.WriteAllLines(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/voznje.txt", file);

        }
    }
}
