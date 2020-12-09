using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OsCoreApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               name: "Binh Duong East",
               url: "bat-dong-san-dong-binh-duong/{*Page}",
               defaults: new { controller = "Project", action = "BinhDuongEast", id = UrlParameter.Optional }
            );


            routes.MapRoute(
              name: "Sale Project",
              url: "bat-dong-san-can-ban/{*Page}",
              defaults: new { controller = "Project", action = "SaleProject", id = UrlParameter.Optional }
            );


            routes.MapRoute(
             name: "Lease Project",
             url: "bat-dong-san-cho-thue/{*Page}",
             defaults: new { controller = "Project", action = "LeaseProject", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Project Deatail",
               url: "du-an/{metatitle}-{id}",
               defaults: new { controller = "Project", action = "ProjectDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Project Binh Duong East Deatail",
              url: "du-an-dong-binh-duong/{metatitle}-{id}",
              defaults: new { controller = "Project", action = "ProjectDetail", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Project Sale Deatail",
              url: "du-an-can-ban/{metatitle}-{id}",
              defaults: new { controller = "Project", action = "ProjectDetail", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Project Lease Deatail",
              url: "du-an-cho-thue/{metatitle}-{id}",
              defaults: new { controller = "Project", action = "ProjectDetail", id = UrlParameter.Optional }
           );


            routes.MapRoute(
              name: "News",
              url: "tin-tuc",
              defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "News Detail",
              url: "tin-tuc/{metatitle}-{id}",
              defaults: new { controller = "News", action = "NewsDetail", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "About",
            url: "gioi-thieu",
            defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
         );


            routes.MapRoute(
             name: "Contact",
             url: "lien-he",
             defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
          );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
