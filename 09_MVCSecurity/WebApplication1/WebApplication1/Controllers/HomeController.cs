using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // User.Identity.IsAuthenticated
            // Request.IsAuthenticated
            // User.Identity.Name
            // User.IsInRole("Admin")

            if (User.Identity.IsAuthenticated == false)
            {

            }

            var user = (ClaimsPrincipal)User;

            
            var emailClaim = user.Claims
                .Where(x => x.Type == "email")
                .FirstOrDefault();
            
            if (emailClaim != null)
            {
                var email = emailClaim.Value;
            }
                
            //User.IsInRole()

            return View();
        }

        [Authorize(Roles="Manager")]
        public ActionResult Secure()
        {
            //User.IsInRole()
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(string username, string returnUrl)
        {
            if (username == "brock")
            {
                // Rfc2898DeriveBytes
                // MembershipReboot
                var claims = new Claim[]{
                    new Claim("name", "Brock"),
                    new Claim("email", "brockallen@gmail.com"),
                    new Claim("id", "123"),
                    new Claim("role", "Admin"),
                    new Claim("role", "Dev"),
                    new Claim("status", "Gold"),
                };
                
                var ci = new ClaimsIdentity(
                    claims,
                    "Cookies", 
                    "name", "role");

                var ctx = Request.GetOwinContext();
                ctx.Authentication.SignIn(ci);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("~/Home");
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View("Login");
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut("Cookies");
            return Redirect("~/");
        }
    }
}