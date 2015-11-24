using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Data
{
	public class SchoolContext : DbContext
	{
		public SchoolContext() : base("UniversityEdu")
		{
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Course> Courses { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
