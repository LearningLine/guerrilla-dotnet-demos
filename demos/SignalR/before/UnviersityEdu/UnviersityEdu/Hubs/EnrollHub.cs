using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.SignalR;

namespace UnviersityEdu.Hubs
{
    public class EnrollHub : Hub
    {
        public void RegisterHello(string first, string last)
        {
            Debug.WriteLine("{0} {1}", first, last);
        }

        public Status[] GetStatuses()
        {
            return GetCourseStatuses();
        }

        private static Status[] GetCourseStatuses()
        {
            return (from id in FakeEnrollmentDb.GetCourseIds()
                select new Status
                {
                    Id = id,
                    Title = FakeEnrollmentDb.GetCourse(id).Title,
                    RemainingSeats = 20 - FakeEnrollmentDb.GetEnrollments(id).Count
                }).ToArray();
        }

        public void EnrollInCourse(int courseId)
        {
            FakeEnrollmentDb.AddEnrollment(courseId);

            Clients.All.courseStatusChanges(GetCourseStatuses());
        }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RemainingSeats { get; set; }
    }
}