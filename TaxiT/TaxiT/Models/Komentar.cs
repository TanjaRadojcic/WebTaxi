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
        public Korisnik Korisnik { get; set; }
        public Voznja Voznja { get; set; }
        public Ocena Ocena { get; set; }//?? 1-5 a 0 ako nema ocene

    }
}