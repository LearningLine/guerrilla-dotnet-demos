using System.Collections.Generic;
using System.Linq;
using UnviersityEdu.Data;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Hubs
{
	static class FakeEnrollmentDb
	{

		static readonly Dictionary<int, Course> courseDictionary =
			new Dictionary<int, Course>();

		static readonly Dictionary<int, List<string>> enrollmentDictionary =
			new Dictionary<int, List<string>>();

		public static Course GetCourse(int id)
		{
			Init();
			return courseDictionary[id];
		}

		public static List<string> GetEnrollments(int id)
		{
			Init();
			return enrollmentDictionary[id];
		}
		
		public static List<int> GetCourseIds()
		{
			Init();
			return courseDictionary.Keys.ToList();
		}

		private static void Init()
		{
			if (enrollmentDictionary.Count > 0)
				return;

			//var ctx = new SchoolContext();
			//var courses = ctx.Courses.OrderBy(c => c.Title).ToList();

			var courses = new Course[]
			{
				new Course() {CourseID=1, Credits = 3, Title = "Differential Equations"}, 
				new Course() {CourseID=2, Credits = 3, Title = "Linear Algebra"}, 
				new Course() {CourseID=3, Credits = 5, Title = "Calculus I"}, 
				new Course() {CourseID=4, Credits = 5, Title = "Calculus II"}, 
				new Course() {CourseID=5, Credits = 5, Title = "Calculus III"}, 
			}.ToList();
			courses.ForEach(c => courseDictionary.Add(c.CourseID, c));

			foreach (var c in courses)
			{
				enrollmentDictionary.Add(c.CourseID, new List<string>());
			}
		}

		public static void AddEnrollment(int id)
		{
			enrollmentDictionary[id].Add("Enrolled");
		}
	}
}