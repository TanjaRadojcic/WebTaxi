using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static TaxiT.Models.Enums;

namespace TaxiT.Models
{
    public class Voznje
    {
        public static Dictionary<int, Voznja> voznje { get; set; } = new Dictionary<int, Voznja>();

        public Voznje(string path)
        {
            path = HostingEnvironment.MapPath(path);
            voznje = new Dictionary<int, Voznja>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Enum.TryParse(tokens[10], out Auto auto);
                Enum.TryParse(tokens[29], out StatusVoznje s);


                Adresa a = new Adresa(Int32.Parse(tokens[5]), tokens[6], tokens[7], tokens[8], Int32.Parse(tokens[9]));
                Lokacija l = new Lokacija(Int32.Parse(tokens[2]), double.Parse(tokens[3]), double.Parse(tokens[4]), a);

                Adresa a2 = new Adresa(Int32.Parse(tokens[15]), tokens[16], tokens[17], tokens[18], Int32.Parse(tokens[19]));
                Lokacija l2 = new Lokacija(Int32.Parse(tokens[12]), double.Parse(tokens[13]), double.Parse(tokens[14]), a2);

                Komentar k = new Komentar(Int32.Parse(tokens[23]), tokens[24], DateTime.Parse(tokens[25]), tokens[26], tokens[27], Int32.Parse(tokens[28]));

                Voznja p = new Voznja(Int32.Parse(tokens[0]), DateTime.Parse(tokens[1]), l, auto, tokens[11], l2, tokens[20], tokens[21], Int32.Parse(tokens[22]), k, s);

                voznje.Add(p.Id, p);
            }
            sr.Close();
            stream.Close();
        }
    }
}