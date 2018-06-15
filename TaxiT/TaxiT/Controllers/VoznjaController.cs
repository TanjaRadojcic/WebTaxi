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
    public class VoznjaController : ApiController
    {
        // GET: api/Voznja
        public Dictionary<int,Voznja> Get()
        {
            return Voznje.voznje;
        }

        // GET: api/Voznja/5
        public Voznja Get(int id)
        {
            return Voznje.voznje[id];
        }

        // POST: api/Voznja
        public bool Post([FromBody]Voznja voznja)
        {
            if(Voznje.voznje == null)
            {
                Voznje.voznje = new Dictionary<int, Voznja>();
            }
            voznja.Id = Voznje.voznje.Count;
            voznja.Datum = DateTime.Now;
            voznja.Komentar = new Komentar();
            voznja.Odrediste = new Lokacija();

            Voznje.voznje.Add(voznja.Id, voznja);
            
            AddToFile(voznja);
            return true;
            
        }

        // PUT: api/Voznja/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Voznja/5
        public void Delete(int id)
        {
        }

        [NonAction]
        public void AddToFile(Voznja v)
        {
            FileStream stream = new FileStream(@"D:\VebProjekat\WebTaxi\TaxiT\TaxiT\App_Data/voznje.txt", FileMode.Append);
            using (StreamWriter outputFile = new StreamWriter(stream))
            {
                string voznja = v.Id + ";" + v.Datum.ToString() + ";"  + v.PocetnaLokacija.Id + ";" + v.PocetnaLokacija.X + ";" + v.PocetnaLokacija.Y + ";" + v.PocetnaLokacija.Adresa.Id + ";" +
                    v.PocetnaLokacija.Adresa.Ulica + ";" + v.PocetnaLokacija.Adresa.Broj + ";" + v.PocetnaLokacija.Adresa.Mesto + ";" + v.PocetnaLokacija.Adresa.Zip + ";" + v.TipAutomobila + ";" + 
                    v.Musterija + ";" + v.Odrediste.Id + ";" + v.Odrediste.X + ";" + v.Odrediste.Y + ";" + v.Odrediste.Adresa.Id + ";" + v.Odrediste.Adresa.Ulica + ";" + v.Odrediste.Adresa.Broj + ";"
                    + v.Odrediste.Adresa.Mesto + ";" + v.Odrediste.Adresa.Zip + ";" + v.Dispecer + ";" + v.Vozac + ";" + v.Iznos + ";" + v.Komentar.Id + ";" + v.Komentar.Opis + ";" + v.Komentar.DatumObjave.ToString()
                    + ";" + v.Komentar.Korisnik + ";" + v.Komentar.Voznja + ";" + v.Komentar.Ocena + ";" + v.Status;
                outputFile.WriteLine(voznja);
            }
            stream.Close();
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
