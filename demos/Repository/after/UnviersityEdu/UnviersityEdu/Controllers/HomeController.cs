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

		public HomeController(IUnitOfWorkFactory factory) : base(factory)
		{
		}

		public ActionResult Index()
		{
			ICourseRepository courseRepo = Db.CourseRepository;
			var courses = courseRepo.Entities.ToArray();

			return View(courses);
		}
	}
}