using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Automobil
    {
        public string Id { get; set; }
        public Vozac Vozac { get; set; }
        public int Godiste { get; set; }
        public string Registracija { get; set; }
        public int BrojVozila { get; set; }
        public Auto TipAutomobila { get; set; }
    }
}