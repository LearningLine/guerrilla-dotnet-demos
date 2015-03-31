using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace prod_consumer
{
	class Program
	{
		//static ConcurrentQueue<int> inputQueue = new ConcurrentQueue<int>();
		static BlockingCollection<int> bc = new BlockingCollection<int>(
			new ConcurrentQueue<int>());
		static Barrier b = new Barrier(4);

		static void Main(string[] args)
		{

			Console.WriteLine("Starting subsystems");
			Thread pt = new Thread(RunProducer);
			pt.IsBackground = true;
			pt.Start();
			pt = new Thread(RunProducer);
			pt.IsBackground = true;
			pt.Start();
			pt = new Thread(RunProducer);
			pt.IsBackground = true;
			pt.Start();

			Thread unblock = new Thread(Unblock);
			unblock.IsBackground = true;
			unblock.Start();

			Thread ct = new Thread(RunConsumer);
			ct.IsBackground = true;
			ct.Start();
			ct = new Thread(RunConsumer);
			ct.IsBackground = true;
			ct.Start();

			Console.WriteLine("Running...");
			Console.ReadLine();
			Console.WriteLine("Done!");
		}

		private static void Unblock()
		{
			b.SignalAndWait();
			Console.WriteLine("All done, signalling compelete");
			bc.CompleteAdding();
		}


		private static void RunProducer()
		{
			Random rand = new Random(0);
			Console.WriteLine("Starting producer...");

			//while (true)
			for (int i =0; i < 100; i++)
			{
				Thread.Sleep(rand.Next(1, 20));
				Console.WriteLine("New request, processed");
				//inputQueue.Enqueue(rand.Next(0, 100));
				bc.Add(rand.Next(0, 100));
			}

			Console.WriteLine("Done producing! {0}", Thread.CurrentThread.ManagedThreadId);

			b.SignalAndWait();
			
		}

		private static void RunConsumer()
		{
			Console.WriteLine("Staring consumer... ");
			foreach (var input in bc.GetConsumingEnumerable())
			{
				Console.WriteLine("Found item " + input);
				Console.WriteLine("Processed item, done.");
			}

			Console.WriteLine("Done consuming!");
			//while (true)
			//{
			//	if (inputQueue.Count == 0)
			//	{
			//		Thread.Sleep(100);
			//		continue;
			//	}

			//	int input = 0;
			//	if (inputQueue.TryDequeue(out input))
			//	{
			//		Console.WriteLine("Found item " + input);
			//		Console.WriteLine("Processed item, done.");
			//	}
			//}
		}


		//static ConcurrentQueue<int> inputQueue = new ConcurrentQueue<int>();

		//static void Main(string[] args)
		//{
		//	Console.WriteLine("Starting subsystems");
		//	Thread pt = new Thread(RunProducer);
		//	pt.IsBackground = true;
		//	pt.Start();

		//	Thread ct = new Thread(RunConsumer);
		//	ct.IsBackground = true;
		//	ct.Start();

		//	Console.WriteLine("Running...");
		//	Console.ReadLine();
		//	Console.WriteLine("Done!");
		//}


		//private static void RunProducer()
		//{
		//	Random rand = new Random(0);
		//	Console.WriteLine("Starting producer...");

		//	while (true)
		//	{
		//		Thread.Sleep(rand.Next(1, 2));
		//		Console.WriteLine("New request, processed");
		//		inputQueue.Enqueue(rand.Next(0, 100));
		//	}
		//}

		//private static void RunConsumer()
		//{
		//	Console.WriteLine("Staring consumer... ");
		//	while (true)
		//	{
		//		if (inputQueue.Count == 0)
		//		{
		//			Thread.Sleep(100);
		//			continue;
		//		}

		//		int input = 0;
		//		if (inputQueue.TryDequeue(out input))
		//		{
		//			Console.WriteLine("Found item " + input);
		//			Console.WriteLine("Processed item, done.");
		//		}
		//	}
		//}
	}
}
