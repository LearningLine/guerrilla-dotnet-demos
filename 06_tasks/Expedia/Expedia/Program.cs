using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Expedia
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();

			//var carTask = new Task<string>(BookCar);
			//carTask.Start();

			//string something = "something";

			var hotelTask = Task.Factory.StartNew<string>(BookHotel);
			var planeTask = Task.Factory.StartNew<string>(BookPlane);
			var carTask = planeTask.ContinueWith(
				tPrev => BookCar2(tPrev.Result), 
				TaskContinuationOptions.OnlyOnRanToCompletion);

			//Console.WriteLine(something);

			//Task.WaitAll(carTask, hotelTask, planeTask);
			//Task.WaitAny()

			
			try
			{
				Console.WriteLine("Pack you bags baby! You trip is booked:");
				Console.WriteLine("Plane: {0}", planeTask.Result);
				Console.WriteLine("Hotel: {0}", hotelTask.Result);
				Console.WriteLine("Car: {0}", carTask.Result);
			}
			catch (AggregateException x)
			{
				foreach (var e in x.Flatten().InnerExceptions)
				{
					Console.WriteLine(e);
				}
			}			

			//GC.Collect();
			//GC.WaitForPendingFinalizers();

			sw.Stop();
			Console.WriteLine("Done in {0} ms", sw.ElapsedMilliseconds);
		}

		private static string BookPlane()
		{
			Console.WriteLine("Booking plane...");
			Thread.Sleep(3000);
			Console.WriteLine("Done with plane");
			return Guid.NewGuid().ToString("N");
		}

		private static string BookHotel()
		{
			Console.WriteLine("Booking hotel...");
			Thread.Sleep(2000);

			throw new Exception("No hotel for you!");

			Console.WriteLine("Done with hotel");
			return Guid.NewGuid().ToString("N");
		}

		private static string BookCar()
		{
			Console.WriteLine("Booking car...");
			Thread.Sleep(5000);
			Console.WriteLine("Done with car");
			return Guid.NewGuid().ToString("N");
		}

		private static string BookCar2(string planeCode)
		{
			Console.WriteLine("Booking car (flight {0}) ...", planeCode);
			Thread.Sleep(5000);
			Console.WriteLine("Done with car");
			return Guid.NewGuid().ToString("N");
		}
	}
}
