using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
            if (Vozaci.vozaci == null)
            {
                Vozaci.vozaci = new Dictionary<int, Vozac>();
            }
            return Vozaci.vozaci;
        }

        // GET: api/Vozac/5
        public Vozac Get(int id)
        {
            if(id >=0 && id <= Vozaci.vozaci.Count)
            {
                return Vozaci.vozaci[id];
            }
            else
            {
                return null;
            }
            
        }

        //registracija
        // POST: api/Vozac
        
        public bool Post([FromBody]Vozac v)
        {
            #region Validacija
            if (String.IsNullOrEmpty(v.KorisnickoIme) || String.IsNullOrEmpty(v.Lozinka) ||
              String.IsNullOrEmpty(v.Ime) || String.IsNullOrEmpty(v.Prezime) ||
              String.IsNullOrEmpty((v.Pol).ToString()) || String.IsNullOrEmpty(v.JMBG) ||
              String.IsNullOrEmpty(v.Kontakt) || String.IsNullOrEmpty(v.Email) ||
              String.IsNullOrEmpty((v.Automobil.Godiste).ToString()) || String.IsNullOrEmpty(v.Automobil.Registracija) ||
              String.IsNullOrEmpty((v.Automobil.BrojVozila).ToString()) || String.IsNullOrEmpty((v.Automobil.TipAutomobila).ToString()))
            {
                return false;
            }


            Regex r1 = new Regex(".{4,13}"); //korisnicko ime i lozinka
            Regex r2 = new Regex("^[^0-9]+$");//ime i prezime
            Regex r3 = new Regex("[0-9]{13}");//jmbg
            Regex r4 = new Regex("[0-9]{6,14}");//kontakt
            Regex r5 = new Regex(@"[a-z0-9._% +-]+@[a-z0-9.-]+\.[a-z]{2,3}$");// email
            Regex r6 = new Regex("[A-Z]{2}[0-9]{3}[A-Z]{2}");//registracija


            if (!r1.IsMatch(v.KorisnickoIme) || !r1.IsMatch(v.Lozinka) || !r2.IsMatch(v.Ime) ||
               !r2.IsMatch(v.Prezime) || !r3.IsMatch(v.JMBG) || !r4.IsMatch(v.Kontakt) ||
               !r5.IsMatch(v.Email) || !r6.IsMatch(v.Automobil.Registracija))
            {
                return false;
            }


            string jmbg = v.JMBG;
            string danRodjenja = jmbg.Substring(0, 2);
            string mesecRodjenja = jmbg.Substring(2, 2);
            string godinaRodjenja = jmbg.Substring(4, 3);
            int idanRodjenja = Int32.Parse(danRodjenja);
            int imesecRodjenja = Int32.Parse(mesecRodjenja);
            int igodinaRodjenja = Int32.Parse(godinaRodjenja);
            if (idanRodjenja <= 0 || idanRodjenja > 31 || imesecRodjenja <= 0 || imesecRodjenja > 12 || (igodinaRodjenja > 18 && igodinaRodjenja < 900))
            {
                return false;
            }

            if(v.Automobil.Godiste <2000 || v.Automobil.Godiste > 2018)
            {
                return false;
            }
            if (v.Automobil.BrojVozila < 0 || v.Automobil.BrojVozila > 100)
            {
                return false;
            }
            #endregion

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
                v.Automobil.Vozac = v.KorisnickoIme;
                Vozaci.vozaci.Add(v.Id, v);

                AddToFileVozac(v);
              

                return true;
            }
            else
            {
                return false;
            }


        }

    
        
        //izmena
        // PUT: api/Vozac/5
        public bool Put(int id, [FromBody]Vozac v)
        {
            //IEnumerable<string> headerValues;
            //var ulogovaniId = string.Empty;
            //if (Request.Headers.TryGetValues("ulogovani", out headerValues))
            //{
            //    ulogovaniId = headerValues.FirstOrDefault();
            //}

            #region Validacija
            if (String.IsNullOrEmpty(v.KorisnickoIme) || String.IsNullOrEmpty(v.Lozinka) ||
              String.IsNullOrEmpty(v.Ime) || String.IsNullOrEmpty(v.Prezime) ||
              String.IsNullOrEmpty((v.Pol).ToString()) || String.IsNullOrEmpty(v.JMBG) ||
              String.IsNullOrEmpty(v.Kontakt) || String.IsNullOrEmpty(v.Email) ||
              String.IsNullOrEmpty((v.Automobil.Godiste).ToString()) || String.IsNullOrEmpty(v.Automobil.Registracija) ||
              String.IsNullOrEmpty((v.Automobil.BrojVozila).ToString()) || String.IsNullOrEmpty((v.Automobil.TipAutomobila).ToString()))
            {
                return false;
            }


            Regex r1 = new Regex(".{4,13}"); //korisnicko ime i lozinka
            Regex r2 = new Regex("^[^0-9]+$");//ime i prezime
            Regex r3 = new Regex("[0-9]{13}");//jmbg
            Regex r4 = new Regex("[0-9]{6,14}");//kontakt
            Regex r5 = new Regex("[a-z0-9._% +-]+@[a-z0-9.-]+.[a-z]{2,3}$");// email
            Regex r6 = new Regex("[A-Z]{2}[0-9]{3}[A-Z]{2}");//registracija


            if (!r1.IsMatch(v.KorisnickoIme) || !r1.IsMatch(v.Lozinka) || !r2.IsMatch(v.Ime) ||
               !r2.IsMatch(v.Prezime) || !r3.IsMatch(v.JMBG) || !r4.IsMatch(v.Kontakt) ||
               !r5.IsMatch(v.Email) || !r6.IsMatch(v.Automobil.Registracija))
            {
                return false;
            }


            string jmbg = v.JMBG;
            string danRodjenja = jmbg.Substring(0, 2);
            string mesecRodjenja = jmbg.Substring(2, 2);
            string godinaRodjenja = jmbg.Substring(4, 3);
            int idanRodjenja = Int32.Parse(danRodjenja);
            int imesecRodjenja = Int32.Parse(mesecRodjenja);
            int igodinaRodjenja = Int32.Parse(godinaRodjenja);
            if (idanRodjenja <= 0 || idanRodjenja > 31 || imesecRodjenja <= 0 || imesecRodjenja > 12 || (igodinaRodjenja > 18 && igodinaRodjenja < 900))
            {
                return false;
            }

            if (v.Automobil.Godiste < 2000 || v.Automobil.Godiste > 2018)
            {
                return false;
            }
            if (v.Automobil.BrojVozila < 0 || v.Automobil.BrojVozila > 100)
            {
                return false;
            }
            #endregion



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
                //Boolean dispecer = Dispeceri.dispeceri.ContainsKey(Int32.Parse(ulogovaniId));
                //if (Int32.Parse(ulogovaniId) == v.Id || dispecer)
                //{
                    Vozaci.vozaci[id] = v;
                    ChangeToFile(v);

               // }

                return true;
            }
            else
            {
                return false;
            }
        }
        

        [NonAction]
        public void ChangeToFile(Vozac v)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/vozaci.txt");
            
            var file = File.ReadAllLines(path);
            file[v.Id] =v.Id + ";" + v.KorisnickoIme + ";" + v.Lozinka + ";" + v.Ime + ";" + v.Prezime + ";" + v.Pol + ";" + v.JMBG + ";" + v.Kontakt + ";" + v.Email
                   + ";" + v.Uloga + ";" + v.Lokacija.Id + ";" + v.Lokacija.X + ";" + v.Lokacija.Y + ";" + v.Lokacija.Adresa.Id + ";" + v.Lokacija.Adresa.Ulica + ";" + v.Lokacija.Adresa.Broj + ";"
                   + v.Lokacija.Adresa.Mesto + ";" + v.Lokacija.Adresa.Zip + ";" + v.Automobil.Id + ";" + v.Automobil.Vozac + ";" + v.Automobil.Godiste + ";" + v.Automobil.Registracija + ";"
                   + v.Automobil.BrojVozila + ";" + v.Automobil.TipAutomobila + ";" + v.Zauzet + ";" + v.Blokiran;

            File.WriteAllLines(path, file);

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
                    + v.Automobil.BrojVozila + ";" + v.Automobil.TipAutomobila + ";" + v.Zauzet + ";" + v.Blokiran;
                outputFile.WriteLine(vozac);
            }
            stream.Close();
        }
    }
}
