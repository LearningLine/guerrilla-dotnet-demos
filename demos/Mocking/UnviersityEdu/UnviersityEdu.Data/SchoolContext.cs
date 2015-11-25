using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public ICourseRepository CourseRepository { get; set; }
        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
