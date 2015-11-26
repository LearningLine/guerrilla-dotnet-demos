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
	    protected IUnitOfWork db;

	    public ControllerFoundation() : this( new EfUnitofWorkFactory())
	    {
            
        }

	    public ControllerFoundation(IUnitOfWorkFactory uwf)
	    {
	        db = uwf.Create();
	    }

		protected override void Dispose(bool disposing)
		{
			//db.Dispose();
			base.Dispose(disposing);
		}
	}
}