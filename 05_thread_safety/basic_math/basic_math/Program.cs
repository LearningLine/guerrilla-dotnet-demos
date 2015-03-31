using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace basic_math
{
	class Program
	{
		private static int total = 0;

		static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();

			List<Thread> threads = new List<Thread>();

			for (int i = 0; i < 10; i++)
			{
				Thread t = new Thread(AddALot);
				t.IsBackground = true;
				t.Start();
				threads.Add(t);
			}

			//foreach (var t in threads)
			//{
			//	t.Join();
			//}
			threads.ForEach(t => t.Join());
			
			sw.Stop();

			Console.WriteLine("The total is {0:N0} in {1} ms", total, sw.ElapsedMilliseconds);
		}

		private static void AddALot()
		{
			Console.WriteLine("Starting thread {0}", Thread.CurrentThread.ManagedThreadId);
			for (int i = 0; i < 1000000; i++)
			{
				// total++;
				// read value at address into reg 0
				// increment reg 0
				// store value at address

				Interlocked.Increment(ref total);
			}
			Console.WriteLine("Done with thread {0}", Thread.CurrentThread.ManagedThreadId);
		}
	}
}













