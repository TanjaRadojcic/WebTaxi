using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
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
            else
            {
                voznja.Musterija = "";
            }
            if(voznja.Dispecer != null)
            {
                voznja.Status = Enums.StatusVoznje.Formirana;
            }
            else
            {
                voznja.Dispecer = "";
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
            else
            {
                voznja.Vozac = "";
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
            if(voznja.Dispecer == null)
            {
                voznja.Dispecer = Voznje.voznje[id].Dispecer;
            }
            if (voznja.Vozac == null)
            {
                voznja.Vozac = Voznje.voznje[id].Vozac;
            }
            if(voznja.Iznos == 0)
            {
                voznja.Iznos = Voznje.voznje[id].Iznos;
            }
            else
            {
                voznja.Status = Enums.StatusVoznje.Uspešna;
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
            string path = HostingEnvironment.MapPath("~/App_Data/vozaci.txt");
           
            var file = File.ReadAllLines(path);
            file[v.Id] = v.Id + ";" + v.KorisnickoIme + ";" + v.Lozinka + ";" + v.Ime + ";" + v.Prezime + ";" + v.Pol + ";" + v.JMBG + ";" + v.Kontakt + ";" + v.Email
                   + ";" + v.Uloga + ";" + v.Lokacija.Id + ";" + v.Lokacija.X + ";" + v.Lokacija.Y + ";" + v.Lokacija.Adresa.Id + ";" + v.Lokacija.Adresa.Ulica + ";" + v.Lokacija.Adresa.Broj + ";"
                   + v.Lokacija.Adresa.Mesto + ";" + v.Lokacija.Adresa.Zip + ";" + v.Automobil.Id + ";" + v.Automobil.Vozac + ";" + v.Automobil.Godiste + ";" + v.Automobil.Registracija + ";"
                   + v.Automobil.BrojVozila + ";" + v.Automobil.TipAutomobila + ";" + v.Zauzet;

            File.WriteAllLines(path, file);

        }

        [NonAction]
        public void AddToFile(Voznja v)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/voznje.txt");
            FileStream stream = new FileStream(path, FileMode.Append);
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
            string path = HostingEnvironment.MapPath("~/App_Data/voznje.txt");
            
            var file = File.ReadAllLines(path);
            file[v.Id] = v.Id + ";" + v.Datum.ToString() + ";" + v.PocetnaLokacija.Id + ";" + v.PocetnaLokacija.X + ";" + v.PocetnaLokacija.Y + ";" + v.PocetnaLokacija.Adresa.Id + ";" +
                    v.PocetnaLokacija.Adresa.Ulica + ";" + v.PocetnaLokacija.Adresa.Broj + ";" + v.PocetnaLokacija.Adresa.Mesto + ";" + v.PocetnaLokacija.Adresa.Zip + ";" + v.TipAutomobila + ";" +
                    v.Musterija + ";" + v.Odrediste.Id + ";" + v.Odrediste.X + ";" + v.Odrediste.Y + ";" + v.Odrediste.Adresa.Id + ";" + v.Odrediste.Adresa.Ulica + ";" + v.Odrediste.Adresa.Broj + ";"
                    + v.Odrediste.Adresa.Mesto + ";" + v.Odrediste.Adresa.Zip + ";" + v.Dispecer + ";" + v.Vozac + ";" + v.Iznos + ";" + v.Komentar.Id + ";" + v.Komentar.Opis + ";" + v.Komentar.DatumObjave.ToString()
                    + ";" + v.Komentar.Korisnik + ";" + v.Komentar.Voznja + ";" + v.Komentar.Ocena + ";" + v.Status;
            File.WriteAllLines(path, file);

        }
    }
}
