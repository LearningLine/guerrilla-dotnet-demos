using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace MvcAsync.Controllers
{
    public class HomeController : Controller
    {
        ChinookEntities entities = new ChinookEntities();
        public ActionResult Index()
        {
            List<author> authors =
                (from a in entities.authors
                where a.state == "CA"
                orderby a.au_lname
                select a)
                .Take(10)
                .ToList();

            return View(authors);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}