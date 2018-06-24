using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Korisnici
    {
        public static Dictionary<int, Korisnik> korisnici { get; set; } = new Dictionary<int, Korisnik>();
        
        public Korisnici(string path)
        {
            path = HostingEnvironment.MapPath(path);
            korisnici = new Dictionary<int, Korisnik>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Enum.TryParse(tokens[5], out Pol pol);
                Enum.TryParse(tokens[9], out Uloga uloga);

                bool blok;
                if (tokens[10] == "True")
                {
                    blok = true;
                }
                else
                {
                    blok = false;
                }

                Korisnik p = new Korisnik(Int32.Parse(tokens[0]), tokens[1], tokens[2], tokens[3], tokens[4], pol, tokens[6], tokens[7], tokens[8], uloga, blok);

                korisnici.Add(p.Id, p);
            }
            sr.Close();
            stream.Close();
        }
    }
}