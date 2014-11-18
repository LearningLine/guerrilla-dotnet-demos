using System.Data.Entity;
using System.Threading.Tasks;
using EFRepository;
using ef_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;

namespace web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _uow;

		public HomeController() : this(new EfUnitOfWork())
		{
			
		}

		public HomeController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public ViewResult Index()
		{
			//using (ChinookEntities entities = new ChinookEntities())

			
			//IGenreRepository repo = new EFGenreRepository();
			using(_uow)
			{
				var gs =
					(from g in _uow.Genres.Items
					orderby g.Name
					select g).Take(5).ToArray();

				return View(gs);
			}
		}

		public ViewResult Genre(int id)
		{
			using(_uow)
			{
				var genre = _uow.Genres.FindById(id);
				ViewBag.Genre = genre.Name;

				var albums = _uow.Genres.FindAlbumsByGenre(id);
				

				return View(albums);
			}
		}

	}
}