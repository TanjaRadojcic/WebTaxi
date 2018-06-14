using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TaxiT.Models
{
    public class Lokacije
    {
        public static Dictionary<int, Lokacija> lokacije { get; set; } = new Dictionary<int, Lokacija>();

        public Lokacije(string path)
        {
            path = HostingEnvironment.MapPath(path);
            lokacije = new Dictionary<int, Lokacija>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');

                Adresa a = new Adresa(Int32.Parse(tokens[3]), tokens[4], tokens[5], tokens[6], Int32.Parse(tokens[7]));
                Lokacija p = new Lokacija(Int32.Parse(tokens[0]), Double.Parse(tokens[1]), Double.Parse(tokens[2]), a);

                lokacije.Add(p.Id, p);
            }
            sr.Close();
            stream.Close();
        }
    }
}