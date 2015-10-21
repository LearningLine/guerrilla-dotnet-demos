using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Polling.WebApp.Startup))]
namespace Polling.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
