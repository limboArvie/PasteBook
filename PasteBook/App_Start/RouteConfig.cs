using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PasteBook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "PasteBookApp",
            //    "PasteBookApp/{userID}",
            //    new { controller = "PasteBookApp", action = "UserProfile" },
            //    new { userID = @"\w+" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PasteBookAccount", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
