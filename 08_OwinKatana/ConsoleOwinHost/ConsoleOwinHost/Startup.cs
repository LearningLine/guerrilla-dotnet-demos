using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleOwinHost
{
    public static class LoggingMiddlewareExtensions
    {
        public static void UseLogging(this IAppBuilder app, LoggingOptions options = null)
        {
            app.Use<LoggingMiddleware>(options);
        }
    }

    public class LoggingOptions
    {
        public bool EnableLogging { get; set; }
    }

    public class LoggingMiddleware
    {
        Func<IDictionary<string, object>, Task> _next;
        LoggingOptions _options;
        public LoggingMiddleware(Func<IDictionary<string, object>, Task> next, LoggingOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var ctx = new OwinContext(env);
            if (_options.EnableLogging)
            {
                Console.WriteLine("new and improved! {0}", ctx.Request.Uri.AbsoluteUri);
            }

            await _next(env);

            if (_options.EnableLogging)
            {
                Console.WriteLine("Response: {0}", ctx.Response.StatusCode);
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                FileSystem = new PhysicalFileSystem(@"c:\demos"),
                RequestPath = new PathString("/demos")
            });

            app.UseLogging(new LoggingOptions
            {
                EnableLogging = true
            });

            //app.Use<LoggingMiddleware>(new LoggingOptions { 
            //    EnableLogging = false
            //});

            //app.Use(async (ctx, next) =>
            //{
            //    Console.WriteLine(ctx.Request.Uri.AbsoluteUri);
            //    await next();
            //    Console.WriteLine("Response: {0}", ctx.Response.StatusCode);
            //});

            app.Map("/foo", foo =>
            {
                foo.Use(async (ctx, next) =>
                {
                    await ctx.Response.WriteAsync("<h1>Hello FOO!</h1>");
                });
            });

            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("<h1>Hello OWIN!</h1>");
            });
        }
    }
}
