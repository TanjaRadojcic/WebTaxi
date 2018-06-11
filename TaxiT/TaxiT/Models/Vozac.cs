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
    }
}