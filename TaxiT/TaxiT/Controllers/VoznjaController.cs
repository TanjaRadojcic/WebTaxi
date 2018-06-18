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
            voznja.Datum = DateTime.Now.Date + DateTime.Now.TimeOfDay;
            voznja.Komentar = new Komentar();
            voznja.Odrediste = new Lokacija();
            if(voznja.Musterija != null)
            {
                voznja.Status = Enums.StatusVoznje.Kreirana;
            }
            if(voznja.Dispecer != null)
            {
                voznja.Status = Enums.StatusVoznje.Formirana;
            }
            if(voznja.Vozac!= null)
            {
                foreach(var v in Vozaci.vozaci.Values)
                {
                    if(v.KorisnickoIme == voznja.Vozac)
                    {
                        v.Zauzet = true;
                        ChangeToFileVozac(v);
                    }
                }
            }
            Voznje.voznje.Add(voznja.Id, voznja);
            
            AddToFile(voznja);
            return true;
            
        }

        // PUT: api/Voznja/5
        public bool Put(int id, [FromBody]Voznja voznja)
        {
            voznja.Id = id;
            voznja.Datum = Voznje.voznje[id].Datum;
            voznja.Musterija = Voznje.voznje[id].Musterija;
            if (voznja.PocetnaLokacija == null)
            {
                voznja.PocetnaLokacija = Voznje.voznje[id].PocetnaLokacija;
            }
            voznja.Status = Voznje.voznje[id].Status;
            if (voznja.Komentar == null)
            {
                voznja.Komentar = new Komentar();
            }
            if (voznja.Odrediste == null)
            {
                voznja.Odrediste = new Lokacija();
            }
            if(voznja.Vozac != null)
            {
                foreach (var v in Vozaci.vozaci.Values)
                {
                    if (v.KorisnickoIme == voznja.Vozac)
                    {
                        v.Zauzet = true;
                        ChangeToFileVozac(v);
                    }
                }
            }

            if (Voznje.voznje != null)
            {
                Voznje.voznje[id] = voznja;
                ChangeToFile(voznja);
                return true;
            }
            return false;
        }
        

        // DELETE: api/Voznja/5
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
