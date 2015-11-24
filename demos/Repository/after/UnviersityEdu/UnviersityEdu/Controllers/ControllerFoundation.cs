using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnviersityEdu.Data;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Controllers
{
	public class ControllerFoundation : Controller
	{
		protected IUnitOfWork Db;

		public ControllerFoundation(IUnitOfWorkFactory factory)
		{
			Db = factory.Create();
		}

		public ControllerFoundation() : this(new EfUnitOfWorkFactory())
		{
		}

		protected override void Dispose(bool disposing)
		{
			Db.Dispose();
			base.Dispose(disposing);
		}
	}
}