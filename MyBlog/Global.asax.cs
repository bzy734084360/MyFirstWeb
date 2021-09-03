using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BaseConfiguration.GetSetting();//初始化配置文件

            //替换容器的创建
            var container = MyBlogContainer.GetContainer();
            //解析器的替换
            //DependencyResolver.SetResolver(new MyBlogDependencyResolver(container));
            //替换控制器工厂
            ControllerBuilder.Current.SetControllerFactory(new MyBlogControllerFactory(container));
        }
    }
}
