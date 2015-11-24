using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Tests
{
	class TestUnitOfWorkFactory  : IUnitOfWorkFactory
	{
		public IUnitOfWork Create()
		{
			return new TestUoW();
		}
	}

	internal class TestUoW : IUnitOfWork
	{
		public TestUoW()
		{
			CourseRepository = new TestCourseRepository();
		}

		public void Dispose()
		{
			
		}

		public ICourseRepository CourseRepository { get; private set; }
		public IStudentRepository StudentRepository { get; }
		public IEnrollmentRepository EnrollmentRepository { get; }
	}

	class TestCourseRepository : ICourseRepository
	{
		public IQueryable<Course> Entities {
			get
			{
				return new[]
				{
					new Course() {Title = "c1", Credits = 7},
					new Course() {Title = "c2", Credits = 7}
				}.AsQueryable();
			}
		}
		public void Add(Course t)
		{
			throw new NotImplementedException();
		}

		public void Remove(Course t)
		{
			throw new NotImplementedException();
		}
	}
}
