using System.Web.Mvc;

namespace FirstWeb.Controllers
{
    public class HomeController:Controller
    {
        public ActionResult Index()
        {
            //return Content("Hello");

            return View();
        }
        public ActionResult GoodBye(string id)
        {
            return Content("<html><i>Good Bye " + id + "<id></html>");
        }
    }


}