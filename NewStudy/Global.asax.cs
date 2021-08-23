using BusinessLayer;
using Bzy.Utilities;
using NewStudy.FormStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NewStudy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BusinessSettings.SetBusiness();
            BaseConfiguration.GetSetting();//初始化配置文件
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.User = HttpFormsAuthentication.TryParsePrincipal<UserData>(HttpContext.Current);
        }
    }
}
