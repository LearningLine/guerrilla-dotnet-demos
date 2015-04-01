using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSO.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Login()
        {
            return Redirect("~/");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("Cookies");
            return Redirect("~/");
        }
    }
}