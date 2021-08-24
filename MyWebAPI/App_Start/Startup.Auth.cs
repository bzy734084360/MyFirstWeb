using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MyWebAPI.Auth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebAPI
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,//允许客户端一http协议请求,在生产模式下设 AllowInsecureHttp = false
                TokenEndpointPath = new PathString("/token"),//token请求的地址，即http://localhost:端口号/token；
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),//token过期时间；
                Provider = new SimpleAuthorizationServerProvider(),//提供具体的认证策略;
                RefreshTokenProvider = new SimpleRefreshTokenProvider()//刷新token
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}