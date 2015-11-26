using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Data.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			ContextKey = "UnviersityEdu.Data.SchoolContext";
		}

		protected override void Seed(SchoolContext context)
		{
			context.Students.AddOrUpdate(s => s.FirstMidName,
				new Student {FirstMidName = "Sarah", LastName = "Jones", Age = 21, EnrollmentDate = DateTime.Now},
				new Student {FirstMidName = "Jeff", LastName = "Thomas", Age = 22, EnrollmentDate = DateTime.Now},
				new Student {FirstMidName = "Larry", LastName = "Berg", Age = 20, EnrollmentDate = DateTime.Now}
				);

			context.Courses.AddOrUpdate(
				c => c.Title,
				new Course {Title = "Quantum mechanics I", Credits = 5},
				new Course {Title = "Quantum mechanics II", Credits = 5},
				new Course {Title = "Differential equations", Credits = 3},
				new Course {Title = "Organic Chemistry", Credits = 3}
				);

			context.Enrollments.ToList().ForEach(e => context.Enrollments.Remove(e));

		    context.SaveChanges();

			context.Enrollments.Add(new Enrollment()
			{
				CourseID = context.Courses.First().CourseID,
				StudentID = context.Students.OrderBy(s => s.ID).First().ID,
			});

			context.Enrollments.Add(new Enrollment()
			{
				CourseID = context.Courses.First().CourseID,
				StudentID = context.Students.OrderBy(s => s.ID).Skip(1).First().ID,
			});

			context.SaveChanges();
		}
	}
}