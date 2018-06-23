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
        public int Musterija { get; set; }
        public Lokacija Odrediste { get; set; }
        public int Dispecer { get; set; }
        public int Vozac { get; set; }
        public int Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public StatusVoznje Status { get; set; }

        public Voznja() { }
        public Voznja(int id, DateTime d,Lokacija p,Auto tip, int musterija,Lokacija k, int dispecer, int vozac,int iznos,Komentar kom, StatusVoznje st)
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