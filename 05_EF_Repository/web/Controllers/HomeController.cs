using System.Data.Entity;
using System.Threading.Tasks;
using ef_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
	public class HomeController : Controller
	{
		public ViewResult Index()
		{
			using (ChinookEntities entities = new ChinookEntities())
			{
				var gs =
					(from g in entities.Genres
					orderby g.Name
					select g).ToArray();

				return View(gs);
			}
		}

		public ViewResult Genre(int id)
		{
			ChinookEntities entities = new ChinookEntities();
			{
				var genre = entities.Genres.Single(g => g.GenreId == id);
				ViewBag.Genre = genre.Name;

				var albums =
					(from a in entities.Albums
					where a.Tracks.Any(t => t.GenreId == id)
					orderby a.Title
					 select a).ToArray();

				return View(albums);
			}
		}

	}
}