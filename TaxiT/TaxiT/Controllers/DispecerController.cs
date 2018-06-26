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
    public class DispecerController : ApiController
    {
        // GET: api/Dispecer
        public Dictionary<int,Dispecer> Get()
        {
            if(Dispeceri.dispeceri == null)
            {
                Dispeceri.dispeceri = new Dictionary<int, Dispecer>();
            }
            return Dispeceri.dispeceri;
        }

        // GET: api/Dispecer/5
        public Dispecer Get(int id)
        {
            if (id >= 0 && id <= Dispeceri.dispeceri.Count)
            {
                return Dispeceri.dispeceri[id];
            }
            else
            {
                return null;
            }
            
        }
        

        // PUT: api/Dispecer/5
        public bool Put(int id, [FromBody]Dispecer k)
        {
            #region validacija
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
            Regex r5 = new Regex("[a-z0-9._% +-]+@[a-z0-9.-]+.[a-z]{2,3}$");// email


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
            if (Dispeceri.dispeceri == null)
            {
                Dispeceri.dispeceri = new Dictionary<int, Dispecer>();
            }
            foreach (Dispecer dispecer in Dispeceri.dispeceri.Values)
            {
                if (k.KorisnickoIme == dispecer.KorisnickoIme && k.Id != dispecer.Id)
                {
                    postoji = true;
                    break;
                }
            }
            if (!postoji)
            {
                
                Dispeceri.dispeceri[id] = k;
                ChangeToFile(k);

                return true;
            }
            else
            {
                return false;
            }
        }
        

        [NonAction]
        public void ChangeToFile(Dispecer k)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/dispeceri.txt");
            
            var file = File.ReadAllLines(path);
                file[k.Id] = k.Id + ";" + k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.JMBG + ";" + k.Kontakt + ";" + k.Email + ";" + k.Uloga;
                File.WriteAllLines(path, file);
            
        }
    }
}
