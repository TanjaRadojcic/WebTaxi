using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaxiT.Models;

namespace TaxiT
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Dispeceri dispeceri = new Dispeceri("~/App_Data/dispeceri.txt");
            
            Korisnici korisnici = new Korisnici("~/App_Data/korisnici.txt");

            Vozaci vozaci = new Vozaci("~/App_Data/vozaci.txt");

        }
    }
}
