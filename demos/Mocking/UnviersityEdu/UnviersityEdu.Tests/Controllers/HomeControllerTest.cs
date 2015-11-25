using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnviersityEdu.Controllers;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
		    IUnitOfWorkFactory uwfDummy = new TestUnitOfWorkFactory();
			// Arrange
			HomeController controller = new HomeController(uwfDummy);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
        
	}
}

