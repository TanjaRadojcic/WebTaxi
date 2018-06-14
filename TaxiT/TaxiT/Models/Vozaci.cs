using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Vozaci
    {
        public static Dictionary<int, Vozac> vozaci { get; set; } = new Dictionary<int, Vozac>();
        public Vozaci(string path)
        {
            path = HostingEnvironment.MapPath(path);
            vozaci = new Dictionary<int, Vozac>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Enum.TryParse(tokens[5], out Pol pol);
                Enum.TryParse(tokens[9], out Uloga uloga);
                Enum.TryParse(tokens[23], out Auto tipA);

                Adresa a = new Adresa(Int32.Parse(tokens[13]), tokens[14], tokens[15], tokens[16], Int32.Parse(tokens[17]));
                Lokacija l = new Lokacija(Int32.Parse(tokens[10]), double.Parse(tokens[11]), double.Parse(tokens[12]), a);
                Automobil auto = new Automobil(Int32.Parse(tokens[18]), tokens[19], Int32.Parse(tokens[20]), tokens[21], Int32.Parse(tokens[22]), tipA);
                bool zauzet;
                if (tokens[24] == "true")
                {
                    zauzet = true;
                }
                else
                {
                    zauzet = false;
                }
                    

                Vozac p = new Vozac(Int32.Parse(tokens[0]), tokens[1], tokens[2], tokens[3], tokens[4], pol, tokens[6], tokens[7], tokens[8], uloga,l,auto,zauzet);
                vozaci.Add(p.Id, p);
            }
            sr.Close();
            stream.Close();
        }
    }
}