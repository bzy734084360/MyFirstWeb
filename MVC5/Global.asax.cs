using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace MVC5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //注册针对于WinPhone设备模式
            DisplayModeProvider.Instance.Modes.Insert(0, new
                DefaultDisplayMode("WinPhone")
            {
                ContextCondition = (t => t.GetOverriddenUserAgent().IndexOf("Windows Phone OS", StringComparison.OrdinalIgnoreCase) >= 0)
            });
        }
    }
}
