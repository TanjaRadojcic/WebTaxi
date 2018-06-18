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
    public class VozacController : ApiController
    {
        // GET: api/Vozac
        public Dictionary<int,Vozac> Get()
        {
            return Vozaci.vozaci;
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
                if (v.KorisnickoIme == vozac.KorisnickoIme || v.Automobil.BrojVozila == vozac.Automobil.BrojVozila)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                v.Id = Vozaci.vozaci.Count;
                v.Uloga = Enums.Uloga.Vozač;
                v.Automobil.Id = Vozaci.vozaci.Count;
                v.Automobil.Vozac = v.KorisnickoIme;
                v.Lokacija.Id = Vozaci.vozaci.Count;
                Vozaci.vozaci.Add(v.Id, v);

                AddToFileVozac(v);
              

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
            string path = HostingEnvironment.MapPath("~/App_Data/vozaci.txt");
            FileStream stream = new FileStream(path, FileMode.Append);
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
        
        //izmena
        // PUT: api/Vozac/5
        public bool Put(int id, [FromBody]Vozac v)
        {
            bool postoji = false;
            if (Vozaci.vozaci == null)
            {
                Vozaci.vozaci = new Dictionary<int, Vozac>();
            }
            foreach (Vozac vozac in Vozaci.vozaci.Values)
            {
                if (v.KorisnickoIme == vozac.KorisnickoIme && id != vozac.Id)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                Vozaci.vozaci[id] = v;
                ChangeToFile(v);

                return true;
            }
            else
            {
                return false;
            }
        }

        // DELETE: api/Vozac/5
        public void Delete(int id)
        {
        }

        [NonAction]
        public void ChangeToFile(Vozac v)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/vozaci.txt");
            
            var file = File.ReadAllLines(path);
            file[v.Id] =v.Id + ";" + v.KorisnickoIme + ";" + v.Lozinka + ";" + v.Ime + ";" + v.Prezime + ";" + v.Pol + ";" + v.JMBG + ";" + v.Kontakt + ";" + v.Email
                   + ";" + v.Uloga + ";" + v.Lokacija.Id + ";" + v.Lokacija.X + ";" + v.Lokacija.Y + ";" + v.Lokacija.Adresa.Id + ";" + v.Lokacija.Adresa.Ulica + ";" + v.Lokacija.Adresa.Broj + ";"
                   + v.Lokacija.Adresa.Mesto + ";" + v.Lokacija.Adresa.Zip + ";" + v.Automobil.Id + ";" + v.Automobil.Vozac + ";" + v.Automobil.Godiste + ";" + v.Automobil.Registracija + ";"
                   + v.Automobil.BrojVozila + ";" + v.Automobil.TipAutomobila + ";" + v.Zauzet;

            File.WriteAllLines(path, file);

        }
    }
}
