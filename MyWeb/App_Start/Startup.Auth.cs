using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MyWeb.Provider;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeb
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {

            /*
             * OAuthAuthorizationServerOptions  方法说明
             * ①终结点地址：
             * AuthorizeEndpointPath 获取授权码的地址
             * TokenEndpointPath 获取Token的地址
             * ②Token提供器：
             * AuthorizationCodeProvider、AccessTokenProvider、RefreshTokenProvider 完成对应令牌的创建和处理功能
             * ③Token的“加密”与“解密”：
             * ④OAuth授权服务：
             * Provider
             * Provider是整个OAuth服务器的核心，它包含了终结点的处理与响应、OAuth中的4种Access Token授权方式
             * 和刷新令牌获取Access Token的方式以及请求、客户端的相关验证
             * 
             */

            //Owin中间件添加一个授权服务器
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AuthorizeEndpointPath = new PathString("/oauth2/authorize"),
                TokenEndpointPath = new PathString("/oauth2/token"),
                Provider = new BzyOAuthAuthorizationServerProvider(),
                AuthorizationCodeProvider = new AuthorizationCodeProvider(),
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
            });
        }
    }
}