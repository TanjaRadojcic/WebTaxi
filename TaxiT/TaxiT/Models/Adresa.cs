using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiT.Models
{
    public class Adresa
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        public int Broj { get; set; }
        public string Mesto { get; set; }
        public int Zip { get; set; }
    }
}