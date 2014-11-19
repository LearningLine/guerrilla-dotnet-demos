using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcAsync.Startup))]
namespace MvcAsync
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
