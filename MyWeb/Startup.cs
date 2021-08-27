using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyWeb.Startup))]
namespace MyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}