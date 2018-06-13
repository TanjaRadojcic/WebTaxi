using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TaxiT.Models
{
    public class Adrese
    {
        public static Dictionary<int, Adresa> adrese { get; set; } = new Dictionary<int, Adresa>();
        public Adrese(string path)
        {
            path = HostingEnvironment.MapPath(path);
            adrese = new Dictionary<int, Adresa>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
           

                Adresa a = new Adresa(Int32.Parse(tokens[0]), tokens[1], tokens[2], tokens[3], Int32.Parse(tokens[4]));
                adrese.Add(a.Id, a);
            }
            sr.Close();
            stream.Close();
        }
    }
}