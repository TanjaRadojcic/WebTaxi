using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TaxiT.Models
{
    public class Komentari
    {
        public static Dictionary<int, Komentar> komentari { get; set; } = new Dictionary<int, Komentar>();
        public Komentari(string path)
        {
            path = HostingEnvironment.MapPath(path);
            komentari = new Dictionary<int, Komentar>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                

                Komentar a = new Komentar(Int32.Parse(tokens[0]), tokens[1], DateTime.Parse(tokens[2]), tokens[3],tokens[4], Int32.Parse(tokens[5]));
                komentari.Add(a.Id, a);
            }
            sr.Close();
            stream.Close();
        }
    }
}