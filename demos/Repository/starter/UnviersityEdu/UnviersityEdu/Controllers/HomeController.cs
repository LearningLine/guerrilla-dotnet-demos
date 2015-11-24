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
		public ActionResult Index()
		{
			Console.WriteLine("Getting courses");
			var courses = db.Courses.ToArray();

			return View(courses);
		}
	}
}