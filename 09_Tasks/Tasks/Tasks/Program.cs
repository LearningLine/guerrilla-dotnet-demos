using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
	class Program
	{
		static void Main(string[] args)
		{
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			List<Task<int>> tasks = new List<Task<int>>();
			for (int i = 0; i < 10; i++)
			{
				var task = new Task<int>(x => UnitOfWork(x, tokenSource.Token), i, tokenSource.Token);
				task.Start();
				tasks.Add(task);
				task.ContinueWith(t =>
				{
					

					Console.WriteLine("Completed {0}", t.Result);
				});
				Task.Delay(200).Wait();
			}

			tasks.ElementAt(3).ContinueWith(t =>
			{
				Console.WriteLine("In continue");
			});

			var notDone = tasks.Any(t => !t.IsCompleted);
			//if (notDone)
			//	tokenSource.Cancel();
			Console.WriteLine("Has Pending tasks {0}", notDone);

			Console.ReadLine();
		}

		private static int UnitOfWork(object x, CancellationToken token)
		{
			if ((int) x % 2 == 0)
			{
				throw new ArgumentException("I hate evens");
			}
			if ((int) x == 3)
			{
				Task.Factory.StartNew(() =>
				{
					//while (true)
					{
						Thread.Sleep(5000);
						Console.WriteLine("Finishing {0}", x);
						Console.WriteLine("Hello from loop thread: {0}", Thread.CurrentThread.ManagedThreadId);
						//token.ThrowIfCancellationRequested();
						//if (token.IsCancellationRequested)
						//{
						//	Console.WriteLine("Cancelled");
						//	break;
						//}
					}
				}, TaskCreationOptions.AttachedToParent);
			}
			Console.WriteLine("Hello task {0}", x);
			Console.WriteLine("Hello from work thread: {0}", Thread.CurrentThread.ManagedThreadId);
			return DateTime.Now.Second;
		}

		//try
		//{
		//	var result = task.Result;
		//	Console.WriteLine(result);
		//}
		//catch (AggregateException ex)
		//{
		//	//Console.WriteLine(ex);
		//}

	}
}
