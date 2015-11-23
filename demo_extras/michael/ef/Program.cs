using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	class Program
	{
		static void Main(string[] args)
		{
			SchoolContext db = new SchoolContext();
			Console.WriteLine("Getting courses");
			var courses = db.Courses.ToList();
			Console.WriteLine(courses.Count);

			Course c = new Course();
			c.Title = "The course II";
			c.Credits = 5;
			db.Courses.Add(c);
			db.SaveChanges();
			Console.WriteLine("Done");
		}
	}

	public class SchoolContext : DbContext
	{

		public SchoolContext() : base("SchoolContext")
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

	public class Student
	{
		public int ID { get; set; }
		public int Age { get; set; }
		public string LastName { get; set; }
		public string FirstMidName { get; set; }
		public DateTime EnrollmentDate { get; set; }

		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}


	public enum Grade
	{
		A, B, C, D, F
	}

	public class Enrollment
	{
		public int EnrollmentID { get; set; }
		public int CourseID { get; set; }
		public int StudentID { get; set; }
		public Grade? Grade { get; set; }

		public virtual Course Course { get; set; }
		public virtual Student Student { get; set; }
	}

	public class Course
	{
		[Key]
		public int CourseID { get; set; }
		public string Title { get; set; }
		public int Credits { get; set; }

		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}

	//	<connectionStrings>
	//	<add name = "SchoolContext"
	//                connectionString="Data Source=.\SQLExpress;Initial Catalog=SampleUniversity;Integrated Security=SSPI;" 
	//			 providerName="System.Data.SqlClient"/>
	//</connectionStrings>

}
