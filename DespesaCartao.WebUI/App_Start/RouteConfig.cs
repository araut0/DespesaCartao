using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DespesaCartao
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: null, url: "{controller}/{action}/{id}");

            routes.MapRoute(name: null, url: "{controller}/{action}");

            routes.MapRoute(name: null, url: "", defaults: new { controller = "Home", action = "Index" });
        }
    }
}
