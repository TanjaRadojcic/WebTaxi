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
                Voznje.voznje[id].Status = Enums.StatusVoznje.Otkazana;
                ChangeToFile(Voznje.voznje[id]);
                return true;
            }
            return false;
        }

        // DELETE: api/Voznje/5
        public void Delete(int id)
        {
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
