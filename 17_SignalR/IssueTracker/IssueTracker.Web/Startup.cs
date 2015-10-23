using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IssueTracker.Web.Startup))]
namespace IssueTracker.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
