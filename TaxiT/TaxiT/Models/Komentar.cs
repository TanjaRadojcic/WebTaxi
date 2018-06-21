using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Komentar
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public string Korisnik { get; set; }
        public string Voznja { get; set; }
        public int Ocena { get; set; }

        public Komentar() { Ocena = 0; }
        public Komentar(int id, string o, DateTime d, string k, string v, int oc)
        {
            Id = id;
            Opis = o;
            DatumObjave = d;
            Korisnik = k;
            Voznja = v;
            Ocena = oc;
        }
    }
}