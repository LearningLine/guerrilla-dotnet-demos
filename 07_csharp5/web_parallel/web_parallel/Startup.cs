using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(web_parallel.Startup))]
namespace web_parallel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
