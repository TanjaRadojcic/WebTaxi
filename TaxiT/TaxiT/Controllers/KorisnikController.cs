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
        public Korisnik Get(int id)
        {
            return Korisnici.korisnici[id];
        }

        //registracija
        // POST: api/Korisnik
        public bool Post([FromBody]Korisnik k)
        {
            #region Validacija
            if (String.IsNullOrEmpty(k.KorisnickoIme) || String.IsNullOrEmpty(k.Lozinka) ||
              String.IsNullOrEmpty(k.Ime) || String.IsNullOrEmpty(k.Prezime) ||
              String.IsNullOrEmpty((k.Pol).ToString()) || String.IsNullOrEmpty(k.JMBG) ||
              String.IsNullOrEmpty(k.Kontakt) || String.IsNullOrEmpty(k.Email))
            {
                return false;
            }


            Regex r1 = new Regex(".{4,13}"); //korisnicko ime i lozinka
            Regex r2 = new Regex("^[^0-9]+$");//ime i prezime
            Regex r3 = new Regex("[0-9]{13}");//jmbg
            Regex r4 = new Regex("[0-9]{6,14}");//kontakt
            Regex r5 = new Regex(@"[a-z0-9._% +-]+@[a-z0-9.-]+\.[a-z]{2,3}$");// email
            


            if (!r1.IsMatch(k.KorisnickoIme) || !r1.IsMatch(k.Lozinka) || !r2.IsMatch(k.Ime) ||
               !r2.IsMatch(k.Prezime) || !r3.IsMatch(k.JMBG) || !r4.IsMatch(k.Kontakt) ||
               !r5.IsMatch(k.Email) )
            {
                return false;
            }


            string jmbg = k.JMBG;
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
            
            #endregion


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
            #region Validacija
            if (String.IsNullOrEmpty(k.KorisnickoIme) || String.IsNullOrEmpty(k.Lozinka) ||
              String.IsNullOrEmpty(k.Ime) || String.IsNullOrEmpty(k.Prezime) ||
              String.IsNullOrEmpty((k.Pol).ToString()) || String.IsNullOrEmpty(k.JMBG) ||
              String.IsNullOrEmpty(k.Kontakt) || String.IsNullOrEmpty(k.Email))
            {
                return false;
            }


            Regex r1 = new Regex(".{4,13}"); //korisnicko ime i lozinka
            Regex r2 = new Regex("^[^0-9]+$");//ime i prezime
            Regex r3 = new Regex("[0-9]{13}");//jmbg
            Regex r4 = new Regex("[0-9]{6,14}");//kontakt
            Regex r5 = new Regex(@"[a-z0-9._% +-]+@[a-z0-9.-]+\.[a-z]{2,3}$");// email



            if (!r1.IsMatch(k.KorisnickoIme) || !r1.IsMatch(k.Lozinka) || !r2.IsMatch(k.Ime) ||
               !r2.IsMatch(k.Prezime) || !r3.IsMatch(k.JMBG) || !r4.IsMatch(k.Kontakt) ||
               !r5.IsMatch(k.Email))
            {
                return false;
            }


            string jmbg = k.JMBG;
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

            #endregion

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
            string path = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            FileStream stream = new FileStream(path, FileMode.Append);
            using (StreamWriter outputFile = new StreamWriter(stream))
            {
                string korisnik = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga + ";" + k.Blokiran;
                outputFile.WriteLine(korisnik);
            }
            stream.Close();
        }

        

        [NonAction]
        public void ChangeToFile(Korisnik k)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            
            var file = File.ReadAllLines(path);
            file[k.Id] = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga + ";" + k.Blokiran;
            File.WriteAllLines(path, file);

        }
    }
}
