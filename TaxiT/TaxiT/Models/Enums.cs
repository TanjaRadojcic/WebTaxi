using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiT.Models
{
    public class Enums
    {
        public enum Uloga { Mušterija, Vozač, Dispečer }
        public enum Auto { Putnički, Kombi}
        //public enum Ocena { neocenjen, ocenjen}//1,2,3,4,5} //? enum brojeva
        public enum StatusVoznje { Kreirana,Formirana,Obrađena,Prihvaćena,Otkazana,Uspešna,Neuspešna}
        public enum Pol { Muški, Ženski}
    }
}