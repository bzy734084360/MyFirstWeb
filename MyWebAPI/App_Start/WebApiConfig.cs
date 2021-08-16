using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                //routeTemplate: "api/{controller}/{id}",旧webapi路由请求
                routeTemplate: "api/{controller}/{action}/{id}",//新 webapi路由请求
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
