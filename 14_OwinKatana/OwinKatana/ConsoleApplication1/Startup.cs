using System;
using System.Threading.Tasks;
using ConsoleApplication1;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace ConsoleApplication1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Func<IOwinContext, Task> handler = ctx =>
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                ctx.Response.ContentType = "text/plain";
                return ctx.Response.WriteAsync("Hello world");
            };
            app.Map("/log", a=> {
                a.Use<LoggingMiddleware>();
                a.Run(handler);
            });
            app.Use<MungeTheBodyMiddleware>();
            app.Run(handler);

        }
    }
}
