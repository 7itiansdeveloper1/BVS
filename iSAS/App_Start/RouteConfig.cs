using System.Web.Mvc;
using System.Web.Routing;

namespace ISas.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("Content/{*relpath}");
            //routes.RouteExistingFiles = true;
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
                //defaults: new { controller = "Fee_OnlineTransaction", action = "OnlineTransaction", id = UrlParameter.Optional }
            );
        }
    }
}