using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace TheNetClient
{
	class Status
	{
		public int id { get; set; }
		public string course { get; set; }
		public int enrollments { get; set; }

	}

	class Program
	{
		static void Main(string[] args)
		{
			var conn = new HubConnection("http://localhost:56720/signalr");
			
			var enroll = conn.CreateHubProxy("enrollHub");
			enroll.On<Status[]>("enrollmentStatuses", e =>
			{
				Console.WriteLine("");

				Console.WriteLine("Update on courses:");
				Console.WriteLine("Id\tSlots\tName");
				foreach (Status s in e)
				{
					Console.WriteLine("{0}\t{1}\t{2}", s.id, 200 - s.enrollments, s.course);
				}
				Console.WriteLine();
			});

			conn.Start().Wait();
			Console.ReadLine();
		}
	}
}
