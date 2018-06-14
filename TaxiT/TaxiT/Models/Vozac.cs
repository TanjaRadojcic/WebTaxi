using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Vozac : Korisnik
    {
        
        public Lokacija Lokacija { get; set; }
        public Automobil Automobil { get; set; }
        public bool Zauzet { get; set; }

        public Vozac() { }
        public Vozac(int id, string k, string l, string i, string p, Pol pol, string jmbg, string kontakt, string e, Uloga u, Lokacija lok, Automobil a,bool z)
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
            Lokacija = lok;
            Automobil = a;
            Zauzet = z;
        }
    }
}