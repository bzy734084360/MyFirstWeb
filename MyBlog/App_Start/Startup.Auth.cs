using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using MyBlog.Identity;
using MyBlog.Provider;
using Owin;
using System;

namespace MyBlog
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var issuer = "http://localhost:44354/";
            app.CreatePerOwinContext(BlogIdentityDbContext.Create);
            app.CreatePerOwinContext<BlogUserManager>(BlogUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            //添加Cookie验证的中间件，当未登录访问受限内容时自动跳转登陆页面
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
                {
                    // 当用户登录时使应用程序可以验证安全戳。
                    // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<BlogUserManager, BlogUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            //添加消极模式的外部Cookie身份验证中间件
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 使应用程序可以在双重身份验证过程中验证第二因素时暂时存储用户信息。  **用于处理二次验证
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 使应用程序可以记住第二登录验证因素，例如电话或电子邮件。
            // 选中此选项后，登录过程中执行的第二个验证步骤将保存到你登录时所在的设备上。
            // 此选项类似于在登录时提供的“记住我”选项。 **用于记住登录状态，下次访问系统时，自动登录
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);


            /*
             * OAuth 2.0
             * 为Owin中间件添加一个授权服务器
             * 添加授权服务器终结点 AuthorizeEndpointPath：授权码 TokenEndpointPath：Token
             */
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,//线上服务器切换为False
                AuthorizeEndpointPath = new PathString("/oauth2/authorize"),
                TokenEndpointPath = new PathString("/oauth2/token"),
                Provider = new BlogOAuthAuthorizationServerProvider(),
                AuthorizationCodeProvider = new BlogAuthorizationCodeProvider(),
                RefreshTokenProvider = new BlogRefreshTokenProvider(),
                AccessTokenFormat = new MyBlogJwtFormat(issuer)
            });
            /*
             * 添加基于 Access Token 的身份验证
             */
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
            });
            /*
             * 添加基于JwtBearerAuthentication中间件
             */
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                AllowedAudiences = new[] { "A1D6F8E6-4C63-4D83-893C-DA968CF94ADD", "04061CE0-890F-42B0-8EB6-A3D631250D7B" },//请求资源Client限定
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider(issuer,"MjAyMDEyMjQxOTk0MTIyODE5OTMwMTIxNTIwMTMxNFg="),
                    new SymmetricKeyIssuerSecurityKeyProvider(issuer,"MjAyMDEyMjQxOTk0MTIyODE5OTMwMTIxNTIwMTMxNFk=")
                }
            });


            //添加微软授权登录
            app.UseMicrosoftAccountAuthentication(clientId: "da85c23c-b723-446f-9e98-eea57c5bbfb5", clientSecret: "d_Tw3_jXBUJ26M.Rvhg-l8w-8qylIMvC~2");
        }
    }
}