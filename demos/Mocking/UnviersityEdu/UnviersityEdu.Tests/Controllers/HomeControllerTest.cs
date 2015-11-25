using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnviersityEdu.Controllers;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
	    private Mock<IUnitOfWorkFactory> uwfMock;
	    private Mock<IUnitOfWork> uwMock;
	    private Mock<ICourseRepository> courseRepositoryMock;

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

	    [TestInitialize]
	    public void Setup()
	    {
            uwfMock = new Mock<IUnitOfWorkFactory>();
            uwMock = new Mock<IUnitOfWork>();
            courseRepositoryMock = new Mock<ICourseRepository>();

            uwMock.SetupGet(uw => uw.CourseRepository)
                    .Returns(courseRepositoryMock.Object);

            uwfMock
                .Setup(uwf => uwf.Create())
                .Returns(uwMock.Object);
        }

        [TestMethod]
        public void IndexUsingMoq()
        {
            Course[] expectedCourses = new Course[]
            {
                 new Course() { Credits = 0},
                new Course() { Credits = 9},
                new Course() { Credits = 7},
                 new Course() { Credits = 1},
                new Course() { Credits = 4},
            };

        

            courseRepositoryMock.SetupGet(cr => cr.All)
                .Returns(expectedCourses.AsQueryable());

      


            // Arrange
            HomeController controller = CreateSut();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            var viewModel = (Course[])result.Model;
            // Assert
            Assert.AreEqual(viewModel.Length,expectedCourses.Count(c => c.Credits > 2));
        }

	    [TestMethod]
	    public void Commit_IsNotCalledOnIndex()
	    {
            // Arrange
            HomeController controller = CreateSut();

        
            // Act
	        controller.Index();

            // Assert

            uwMock.Verify(uw => uw.Commit(),Times.Never);
        }

	    [TestMethod]
	    public void Index_WhenOffline_FailsWithNoMonkey()
	    {
            // Arrange
            HomeController controller = CreateSut();

	        courseRepositoryMock
                .SetupGet(cr => cr.All)
	            .Throws<RespositoryException>();

            // Act
	        var result = controller.Index() as RedirectResult;

            Assert.IsNotNull(result);



	    }

        private HomeController CreateSut()
	    {
            return new HomeController(uwfMock.Object);
        }

    }
}

