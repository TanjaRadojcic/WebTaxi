﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Voznja
    {
        public string Id { get; set; }
        public DateTime Datum { get; set; }
        public Lokacija PocetnaLokacija { get; set; }
        public Auto TipAutomobila { get; set; }
        public Korisnik Musterija { get; set; }
        public Lokacija Oderdiste { get; set; }
        public Dispecer Dispecer { get; set; }
        public Vozac Vozac { get; set; }
        public int Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public StatusVoznje Status { get; set; }
    }
}