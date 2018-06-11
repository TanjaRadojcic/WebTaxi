using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiT.Models
{
    public class Lokacija
    {
        public string Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Adresa Adresa { get; set; }
    }
}