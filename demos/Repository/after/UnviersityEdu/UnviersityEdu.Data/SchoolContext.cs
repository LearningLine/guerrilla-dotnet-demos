using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Data
{
	
	public class SchoolContext : DbContext, IUnitOfWork
	{
		public SchoolContext() : base("UniversityEdu")
		{
			CourseRepository = new EfCourseRepository(this);
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Course> Courses { get; set; }

		public ICourseRepository CourseRepository { get; private set; }
		public IStudentRepository StudentRepository { get; private set; }
		public IEnrollmentRepository EnrollmentRepository { get; private set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	public class EfCourseRepository : EfRepository<Course>, ICourseRepository
	{
		public EfCourseRepository(DbContext dbContext) : base(dbContext)
		{
		}
	}

	public class EfRepository<T> : IRepository<T> where T : class
	{
		private readonly DbSet<T> set;

		public EfRepository(DbContext dbContext)
		{
			this.set = dbContext.Set<T>();
		}

		public IQueryable<T> Entities
		{
			get { return set; }
		}

		public void Add(T t)
		{
			set.Add(t);
		}

		public void Remove(T t)
		{
			set.Remove(t);
		}
	}

	public class EfUnitOfWorkFactory : IUnitOfWorkFactory
	{
		public IUnitOfWork Create()
		{
			SchoolContext ctx = new SchoolContext();
			return ctx;
		}
	}
}