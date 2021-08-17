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
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            ConfigureOAuth(app);

            //注册配置
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,//允许客户端一http协议请求,在生产模式下设 AllowInsecureHttp = false
                TokenEndpointPath = new PathString("/token"),//token请求的地址，即http://localhost:端口号/token；
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),//token过期时间；
                Provider = new SimpleAuthorizationServerProvider(),//提供具体的认证策略;
                //刷新token
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}