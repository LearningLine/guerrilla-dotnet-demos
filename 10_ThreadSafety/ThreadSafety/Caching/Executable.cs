using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caching
{
	class Executable
	{
		static ConcurrentQueue<int> q = new ConcurrentQueue<int>();
		static BlockingCollection<int> col = new BlockingCollection<int>(q);
		private static volatile bool quit;

		static void ExeMain(string[] args)
		{
			var t = Task.Factory.StartNew(() => Consume(), TaskCreationOptions.LongRunning);
			Console.WriteLine("Press P to produce and q to Quit");

			int counter = 0;

			do
			{
				while (Console.KeyAvailable == false)
				{
					Thread.Sleep(100);
				}

				var consoleKey = Console.ReadKey(true).Key;
				if (consoleKey == ConsoleKey.Q)
				{
					Console.WriteLine("Quit pressed");
					col.CompleteAdding();
					quit = true;
				}
				else if (consoleKey == ConsoleKey.P)
				{
					col.Add(++counter);

				}
			}
			while (!quit);
			t.Wait();
		}

		private static void Consume()
		{

			foreach (var item in col.GetConsumingEnumerable())
			{
				Console.WriteLine("Starting {0}",item);
				Task.Delay(TimeSpan.FromSeconds(2)).Wait();
				Console.WriteLine("Finishing {0}", item);
			}
		}
	}
}
