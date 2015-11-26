using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using UnviersityEdu.Objects;

namespace UnviersityEdu.Hubs
{
	public class EnrollHub : Hub
	{
		public void Greet()
		{
			Console.WriteLine("Printed");
			Debug.WriteLine("Printed");
		}

		public void EnrollIn(int course)
		{
			FakeEnrollmentDb.AddEnrollment(course);
			SendStatuses();
		}

		public void GetEnrollmentStatus()
		{
			SendStatuses();
		}

		private void SendStatuses()
		{
			Clients.All.enrollmentStatuses(
				(from id in FakeEnrollmentDb.GetCourseIds()
					select new Status
					{
						id = id,
						course = FakeEnrollmentDb.GetCourse(id).Title,
						enrollments = FakeEnrollmentDb.GetEnrollments(id).Count
					}).ToArray()
				);
		}
	}

	class Status
	{
		public int id { get; set; }
		public string course { get; set; }
		public int enrollments { get; set; }

	}
}