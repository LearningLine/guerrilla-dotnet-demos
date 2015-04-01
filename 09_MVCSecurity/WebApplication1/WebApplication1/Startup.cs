using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebApplication1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies",
                    CookieHttpOnly = true,
                    CookieSecure = CookieSecureOption.SameAsRequest,
                    SlidingExpiration = true,
                    ExpireTimeSpan = TimeSpan.FromHours(1),
                    //LoginPath = new PathString("/Home/Login")
                });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Authentication.User != null &&
                    ctx.Authentication.User.Identity.IsAuthenticated)
                {
                    var ci = (ClaimsIdentity)ctx.Authentication.User.Identity;
                    ci.AddClaim(new Claim("custom", "from DB"));
                }

                await next();
                //if (ctx.Response.StatusCode == 401)
                //{
                //    if (ctx.Authentication.User.Identity.IsAuthenticated)
                //    {
                //        ctx.Response.Redirect("/Home/Forbidden");
                //    }
                //    else
                //    {
                //        ctx.Response.Redirect("/Home/Login");
                //    }
                //}
            });
        }

    }
}