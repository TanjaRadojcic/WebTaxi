using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaxiT.Models;

namespace TaxiT.Controllers
{
    public class LogInController : ApiController
    {
        public bool Post([FromBody]Korisnik k)
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as Dictionary<string, Korisnik>;
            var dispeceri = HttpContext.Current.Application["dispeceri"] as Dictionary<string, Dispecer>;
            if(korisnici == null)
            {

            }

            if (korisnici != null)
            {
                foreach (Korisnik korisnik in korisnici.Values)
                {
                    if (k.KorisnickoIme == korisnik.KorisnickoIme && k.Lozinka == korisnik.Lozinka)
                    {
                        HttpContext.Current.Application["ulogovan"] = korisnik;
                        break;
                    }
                }
            }

            if (dispeceri != null)
            {
                
                foreach (Dispecer dispecer in dispeceri.Values)
                {
                    if (k.KorisnickoIme == dispecer.KorisnickoIme && k.Lozinka == dispecer.Lozinka)
                    {
                        HttpContext.Current.Application["ulogovan"] = dispecer;
                        break;
                    }
                }
            }

            if(HttpContext.Current.Application["ulogovan"] != null)
            {
                // HttpContext.Current.Session["ulogovan"] as Korisnik;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
