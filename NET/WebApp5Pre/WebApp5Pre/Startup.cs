using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp5Pre.Startup))]
namespace WebApp5Pre
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
