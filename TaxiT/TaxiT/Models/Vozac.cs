using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Vozac 
    {
        public string Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public string JMBG { get; set; }
        public string Kontakt { get; set; }
        public string Email { get; set; }
        public Uloga Uloga { get; set; }

        public Lokacija Lokacija { get; set; }
        public Automobil Automobil { get; set; }
        public bool Zauzet { get; set; }

        public Vozac(string id, string k, string l, string i, string p, Pol pol, string jmbg, string kontakt, string e, Uloga u, Lokacija lok, Automobil a)
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
        }
    }
}