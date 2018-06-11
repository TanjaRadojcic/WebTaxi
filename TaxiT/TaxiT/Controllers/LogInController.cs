using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class LogInController : ApiController
    {
        public void Post([FromBody]Korisnik k)
        {
            k.Id = (Korisnici.korisnici.Count + 1).ToString();
            k.Uloga = Enums.Uloga.Mušterija;
           // Korisnici.korisnici.Add(k.Id, k);
           // AddToFile(k);

        }
    }
}
