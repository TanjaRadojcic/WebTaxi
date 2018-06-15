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
        public string Broj { get; set; }
        public string Mesto { get; set; }
        public int Zip { get; set; }

        public Adresa() { }
        public Adresa(int id, string u, string b, string m, int z)
        {
            Id = id;
            Ulica = u;
            Broj = b;
            Mesto = m;
            Zip = z;

        }
    }
}