using System;
using System.Linq;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Tests.Controllers
{
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