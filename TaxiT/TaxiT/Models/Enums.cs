using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiT.Models
{
    public class Enums
    {
        public enum Uloga { Mušterija, Dispečer, Vozač }
        public enum Auto {Svejedno,Putnički, Kombi}
        public enum StatusVoznje { Kreirana,Formirana,Obrađena,Prihvaćena,Otkazana,Uspešna,Neuspešna}
        public enum Pol { Muški, Ženski}
    }
}