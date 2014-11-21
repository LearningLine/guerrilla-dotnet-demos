using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chatty.Web.Startup))]
namespace Chatty.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureSignalR(app);
        }
    }
}
