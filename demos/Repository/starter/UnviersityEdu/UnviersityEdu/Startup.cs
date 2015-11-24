using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnviersityEdu.Startup))]
namespace UnviersityEdu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
