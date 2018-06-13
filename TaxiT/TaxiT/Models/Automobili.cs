using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Automobili
    {
        public static Dictionary<int, Automobil> automobili { get; set; } = new Dictionary<int, Automobil>();
        public Automobili(string path)
        {
            path = HostingEnvironment.MapPath(path);
            automobili = new Dictionary<int, Automobil>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Enum.TryParse(tokens[5], out Auto auto);

                Automobil a = new Automobil(Int32.Parse(tokens[0]), tokens[1],Int32.Parse(tokens[2]), tokens[3], Int32.Parse(tokens[4]), auto);
                automobili.Add(a.Id, a);
            }
            sr.Close();
            stream.Close();
        }
    }
}