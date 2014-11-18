using System;
using System.Collections.Generic;
using System.Linq;
using ef_data;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using web.Controllers;

namespace web.Test
{
	[TestClass]
	public class HomeControllerTests
	{
		[TestMethod]
		public void Index_WillReturnOnlyFiveGenres()
		{
			var sut = new HomeController(new TestUnitOfWork());

			var view = sut.Index();

			var genres = (IEnumerable<Genre>) view.Model;

			Assert.AreEqual(5, genres.Count());

		}
	}

	class TestUnitOfWork : IUnitOfWork
	{
		public IGenreRepository Genres
		{
			get { return new TestGenreRepository(); }
		}

		public void Commit()
		{
			
		}

		public void Dispose()
		{
			
		}
	}

	class TestGenreRepository : IGenreRepository
	{
		public void Add(Genre genre)
		{
			throw new NotImplementedException();
		}

		public void Delete(Genre genre)
		{
			throw new NotImplementedException();
		}

		public System.Linq.IQueryable<Genre> Items
		{
			get
			{
				return new List<Genre>
				{
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
				}.AsQueryable();
			}
		}

		public Genre FindById(int id)
		{
			return new Genre();
		}


		public Album[] FindAlbumsByGenre(int id)
		{
			throw new NotImplementedException();
		}
	}


}
