using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MyBlog.Identity;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(MyBlog.Startup))]

namespace MyBlog
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(BlogIdentityDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
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
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            //消极模式的外部Cookie身份验证中间件
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseMicrosoftAccountAuthentication(clientId: "da85c23c-b723-446f-9e98-eea57c5bbfb5", clientSecret: "d_Tw3_jXBUJ26M.Rvhg-l8w-8qylIMvC~2");

        }
    }
}
