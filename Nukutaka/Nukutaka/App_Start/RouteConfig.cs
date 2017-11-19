using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nukutaka
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Category Page 404",
               url: "Category/Category",
               defaults: new { controller = "Home", action = "Page404", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Brand Page 404",
               url: "Brand/Brands",
               defaults: new { controller = "Home", action = "Page404", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Count Page 404",
             url: "Brand/CountProduct",
             defaults: new { controller = "Home", action = "Page404", id = UrlParameter.Optional }
         );

            routes.MapRoute(
               name: "HomePage",
               url: "home-page",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
 
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );       
        }
    }
}
