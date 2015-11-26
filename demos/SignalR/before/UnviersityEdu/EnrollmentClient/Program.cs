using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace EnrollmentClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var hub = new HubConnection("http://localhost:56720/");
            var proxy = hub.CreateHubProxy("EnrollHub");

            hub.Start().Wait();

            proxy.Invoke("EnrollInCourse", 3).Wait();

            proxy.On("courseStatusChanges", dyn =>
            {
                dynamic first = dyn[0];
                Console.WriteLine("{0}: {1}", first.RemainingSeats, first.Title);
            });

            proxy.On<Status[]>("courseStatusChanges", statuses =>
            {
                Status status= statuses[2];
                Console.WriteLine("{0}: {1}", status.RemainingSeats, status.Title);
            });

            Console.ReadLine();
        }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RemainingSeats { get; set; }
    }
}
