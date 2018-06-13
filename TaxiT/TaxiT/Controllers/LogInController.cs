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
            
            if(Korisnici.korisnici == null && Dispeceri.dispeceri == null )
            {
                return false;
            }

            if (Korisnici.korisnici != null)
            {
                foreach (Korisnik korisnik in Korisnici.korisnici.Values)
                {
                    if (k.KorisnickoIme == korisnik.KorisnickoIme && k.Lozinka == korisnik.Lozinka)
                    {
                        
                        return true;
                        
                    }
                }
            }

            if (Dispeceri.dispeceri != null)
            {
                
                foreach (Dispecer dispecer in Dispeceri.dispeceri.Values)
                {
                    if (k.KorisnickoIme == dispecer.KorisnickoIme && k.Lozinka == dispecer.Lozinka)
                    {
                       
                        return true;
                        
                    }
                }
            }

            
            
                return false;
            

        }
    }
}
