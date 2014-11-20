using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ef_data;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using web.Controllers;

namespace web.Test
{
	[TestClass]
	public class HomeControllerTests
	{
		[TestMethod]
		public void Index_WillReturnOnlyFiveGenres()
		{
            var uow = new Mock<IUnitOfWork>(); 

            var repo = new Mock<IGenreRepository>();

		    uow.Setup(u => u.Genres)
		        .Returns(repo.Object);

            repo.Setup(r => r.Items)
                .Returns(new List<Genre>
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
				    }.AsQueryable());


			var sut = new HomeController(uow.Object);

			ViewResult view = sut.Index();

			var genres = (IEnumerable<Genre>) view.Model;

			Assert.AreEqual(5, genres.Count());
		}
	}
}