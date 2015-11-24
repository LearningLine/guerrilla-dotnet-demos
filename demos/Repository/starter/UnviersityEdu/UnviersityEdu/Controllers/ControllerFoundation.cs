using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnviersityEdu.Data;

namespace UnviersityEdu.Controllers
{
	public class ControllerFoundation : Controller
	{
		protected SchoolContext db = new SchoolContext();

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}