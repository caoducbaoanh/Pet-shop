using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebShopPet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebShopPet.Controllers" }
            );

            routes.MapRoute(
                 "HttpMethodConstraintRoute",
                 "ORDERs/DeleteCart",
                 new { Controller = "ORDERs", action = "DeleteCart" },
            new { method = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                 "HttpMethodConstraintRoute1",
                 "ORDERs/UpdateOrder",
                 new { Controller = "ORDERs", action = "UpdateOrder" },
            new { method = new HttpMethodConstraint("POST") });

            routes.MapRoute(
                 "HttpMethodConstraintRoute2",
                 "ORDERs/PayOrder",
                 new { Controller = "ORDERs", action = "PayOrder" },
            new { method = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                "HttpMethodConstraintRoute3",
                "ORDERs/Checkout",
                new { Controller = "ORDERs", action = "Checkout" },
            new { method = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                "HttpMethodConstraintRoute4",
                "ORDERs/LoadTempQuantity",
                new { Controller = "ORDERs", action = "LoadTempQuantity" },
            new { method = new HttpMethodConstraint("GET") });

            routes.MapRoute(
               "HttpMethodConstraintRoute5",
               "Session/Logout",
               new { Controller = "Session", action = "Logout" },
           new { method = new HttpMethodConstraint("GET") });
        }
    }
}
