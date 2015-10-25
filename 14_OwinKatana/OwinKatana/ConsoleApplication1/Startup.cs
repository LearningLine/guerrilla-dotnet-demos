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

            app.Map("/log", a => {
                a.Use<LoggingMiddleware>();
                Func<IOwinContext, Task> p = HandleResponse;
                a.Run(p);
            });
            app.Use<MungeTheBodyMiddleware>();
            app.Run(HandleResponse);

        }

        async Task<Task> HandleResponse(IOwinContext ctx)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            ctx.Response.ContentType = "text/plain";
            return ctx.Response.WriteAsync("Hello world");
        }
    }
}
