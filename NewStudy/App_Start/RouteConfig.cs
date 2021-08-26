using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewStudy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //添加支持特性定义的路由
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Upload",
                url: "Employee/BulkUpload",
                defaults: new { controller = "BulkUpload", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FormStudy", action = "LogIn", id = UrlParameter.Optional }
            );
        }
    }
}
