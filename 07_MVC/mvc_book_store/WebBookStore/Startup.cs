using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBookStore.Startup))]
namespace WebBookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
