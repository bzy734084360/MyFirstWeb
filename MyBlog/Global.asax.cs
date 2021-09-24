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
            BaseConfiguration.GetSetting();//��ʼ�������ļ�

            //�滻�����Ĵ���
            var container = MyBlogContainer.GetContainer();
            //���������滻
            DependencyResolver.SetResolver(new MyBlogDependencyResolver(container));
            //�滻����������
            ControllerBuilder.Current.SetControllerFactory(new MyBlogControllerFactory(container));

            //�Ż����ܣ����WebForm���棬��ʹ��MVC
            ViewEngines.Engines.Clear();
            //���Razor
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
