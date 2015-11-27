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
			//Console.WriteLine("Getting courses");
			//var courses = db.CourseRepository
			//             .All
			//             .HasAtLeastCredits(2)
			//             .ToArray();
			var courses = new Course[]
					 {
				new Course() {CourseID=1, Credits = 3, Title = "Differential Equations"},
				new Course() {CourseID=2, Credits = 3, Title = "Linear Algebra"},
				new Course() {CourseID=3, Credits = 5, Title = "Calculus I"},
				new Course() {CourseID=4, Credits = 5, Title = "Calculus II"},
				new Course() {CourseID=5, Credits = 5, Title = "Calculus III"},
					 }.ToArray();

			return View(courses);
		}
	}
}