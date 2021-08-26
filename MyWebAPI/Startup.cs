using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MyWebAPI.Auth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(MyWebAPI.Startup))]
namespace MyWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var config = new HttpConfiguration();
            //WebApiConfig.Register(config);
            ConfigureOAuth(app);

            ////注册配置
            //app.UseWebApi(config);

        }

    }
}