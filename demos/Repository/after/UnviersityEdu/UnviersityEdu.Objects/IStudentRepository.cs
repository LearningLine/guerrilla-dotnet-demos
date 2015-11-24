using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnviersityEdu.Objects
{
	public interface IRepository<T>
	{
		IQueryable<T> Entities { get; }
		void Add(T t);
		void Remove(T t);
	}

	public interface IStudentRepository : IRepository<Student>
	{

	}

	public interface ICourseRepository : IRepository<Course>
	{
	}

	public interface IEnrollmentRepository : IRepository<Enrollment>
	{
	}

	public interface IUnitOfWork : IDisposable
	{
		ICourseRepository CourseRepository { get; }
		IStudentRepository StudentRepository { get; }
		IEnrollmentRepository EnrollmentRepository { get; }
	}

	public interface IUnitOfWorkFactory
	{
		IUnitOfWork Create();
	}

}
