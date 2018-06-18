using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Voznja
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public Lokacija PocetnaLokacija { get; set; }
        public Auto TipAutomobila { get; set; }//
        public string Musterija { get; set; }
        public Lokacija Odrediste { get; set; }
        public string Dispecer { get; set; }
        public string Vozac { get; set; }
        public int Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public StatusVoznje Status { get; set; }

        public Voznja() { }
        public Voznja(int id, DateTime d,Lokacija p,Auto tip,string musterija,Lokacija k,string dispecer,string vozac,int iznos,Komentar kom, StatusVoznje st)
        {
            Id = id;
            Datum = d;
            PocetnaLokacija = p;
            TipAutomobila = tip;
            Musterija = musterija;
            Odrediste = k;
            Dispecer = dispecer;
            Vozac = vozac;
            Iznos = iznos;
            Komentar = kom;
            Status = st;
        }
    }
}