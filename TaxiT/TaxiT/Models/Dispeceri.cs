using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Dispeceri
    {
        public static Dictionary<int, Dispecer> dispeceri { get; set; } = new Dictionary<int, Dispecer>();
        public Dispeceri(string path)
        {
            path = HostingEnvironment.MapPath(path);
            dispeceri = new Dictionary<int, Dispecer>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Enum.TryParse(tokens[5], out Pol pol);
                Enum.TryParse(tokens[9], out Uloga uloga);
                Dispecer p = new Dispecer(Int32.Parse(tokens[0]), tokens[1], tokens[2], tokens[3], tokens[4], pol, tokens[6], tokens[7], tokens[8], uloga);
                dispeceri.Add(p.Id, p);
            }
            sr.Close();
            stream.Close();
        }
    }
}