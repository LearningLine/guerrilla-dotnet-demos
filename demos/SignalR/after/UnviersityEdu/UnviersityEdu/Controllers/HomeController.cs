using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnviersityEdu.Data;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Controllers
{
	public class HomeController : ControllerFoundation
	{
	    public HomeController()
	    {
	        
	    }

	    public HomeController(IUnitOfWorkFactory uwf):base(uwf)
	    {
	            
	    }
		public ActionResult Index()
		{
			Console.WriteLine("Getting courses");
			var courses = db.CourseRepository
                .All
                .HasAtLeastCredits(2)
                .ToArray();

			return View(courses);
		}
	}
}