using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Dispecer : Korisnik
    {
        
        public Dispecer() { }
        public Dispecer(int id,string k,string l, string i, string p, Pol pol, string jmbg, string kontakt, string e, Uloga u)
        {
            Id = id;
            KorisnickoIme = k;
            Lozinka = l;
            Ime = i;
            Prezime = p;
            Pol = pol;
            JMBG = jmbg;
            Kontakt = kontakt;
            Email = e;
            Uloga = u;
            Voznje = new List<Voznja>();
        }
        
    }
}