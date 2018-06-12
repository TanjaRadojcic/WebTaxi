using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiT.Models
{
    public class Lokacija
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Adresa Adresa { get; set; }

        public Lokacija(int id, double x, double y, Adresa a)
        {
            Id = id;
            X = x;
            Y = y;
            Adresa = a;
        }
    }
}