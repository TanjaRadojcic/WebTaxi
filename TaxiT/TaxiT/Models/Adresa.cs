using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiT.Models
{
    public class Adresa
    {
        public string Id { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Mesto { get; set; }
        public int Zip { get; set; }
    }
}