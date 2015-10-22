using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace ConsoleApplication1
{
    public class LoggingMiddleware
    {
        AppFunc next;

        public LoggingMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var ctx = new OwinContext(env);
            await next(env);
            Console.WriteLine("{0} - {1} returned {2}", DateTime.Now, ctx.Request.Uri, ctx.Response.StatusCode);
            //string.Join(Environment.NewLine, env.Keys));
        }
    }

    public class MungeTheBodyMiddleware
    {
        AppFunc next;

        public MungeTheBodyMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var ctx = new OwinContext(env);
            await ctx.Response.WriteAsync("<h1>Hello from Owin</h1>");
            await next(env);
            await ctx.Response.WriteAsync("<h1>Goodbye from Owin</h1>");
        }
    }

}
