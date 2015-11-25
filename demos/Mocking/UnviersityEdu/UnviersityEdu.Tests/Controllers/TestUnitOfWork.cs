using UnviersityEdu.Objects;

namespace UnviersityEdu.Tests.Controllers
{
	public class TestUnitOfWork : IUnitOfWork
	{
		ICourseRepository courseRepository  = new TestCourseRepository();
		public ICourseRepository CourseRepository { get { return courseRepository;} }
		public void Commit()
		{
			return;
		}
	}
}