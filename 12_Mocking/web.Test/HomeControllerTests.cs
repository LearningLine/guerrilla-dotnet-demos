using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ef_data;
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

			ViewResult view = sut.Index();

			var genres = (IEnumerable<Genre>) view.Model;

			Assert.AreEqual(5, genres.Count());
		}
	}
}