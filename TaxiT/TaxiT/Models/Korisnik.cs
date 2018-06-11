using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        public long JMBG { get; set; }
        public long Kontakt { get; set; }
        public string Email { get; set; }
        public Uloga Uloga { get; set; }
        public List<Voznja> Voznje { get; set; }//??? 
    }
}