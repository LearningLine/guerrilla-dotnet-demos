using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnviersityEdu;
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

    public class TestUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new TestUnitOfWork();
        }
    }

    public class TestUnitOfWork : IUnitOfWork
    {
        ICourseRepository courseRepository  = new TestCourseRepository();
        public ICourseRepository CourseRepository { get { return courseRepository;} }
        public void Commit()
        {
            return;
        }
    }

    public class TestCourseRepository:ICourseRepository
    {
        public IQueryable<Course> All
        {
            get
            {
                return new Course[]
                {
                    new Course() {Title = "Forensics for dummies"},
                    new Course() {Title = "Get away cars second edition"},
                }.AsQueryable();
            }
        }

        public void Add(Course course)
        {
            throw new NotImplementedException();
        }

        public void Remove(Course course)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Course> GetActiveCourses()
        {
            throw new NotImplementedException();
        }
    }
}

