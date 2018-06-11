using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiT.Models
{
    public class Lokacija
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Adresa Adresa { get; set; }
    }
}