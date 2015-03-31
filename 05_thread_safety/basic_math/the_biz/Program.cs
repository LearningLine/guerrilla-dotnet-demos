using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace the_biz
{
	class Program
	{
		
		private static SmallBusiness biz = new SmallBusiness();

		static void Main(string[] args)
		{
			List<Thread> threads = new List<Thread>();

			for (int i = 0; i < 10; i++)
			{
				//DoSomeTransfers();
				var t = new Thread(DoSomeTransfers);
				t.Start();
				threads.Add(t);
			}

			threads.ForEach(t => t.Join());

			Console.WriteLine("In the end, the net worth is ${0:N0}", biz.NetWorth);
		}

		private static void DoSomeTransfers()
		{
			for (int i = 0; i < 100; i++)
			{
				if (i%50 == 0)
				{
					Console.WriteLine("  -> Net worth {0:N0}", biz.NetWorth);
				} 
				biz.ReceivePayment(1);
			}
		}

	}

	class SmallBusiness
	{
		private object lockObject = new object();
		
		int Cash = 0;
		int Receivables = 1000000;

		private bool hasBadReturn = false;

		public void ReceivePayment(int amount)
		{
			//Monitor.Enter(lockObject);

			lock (lockObject)
			{
				// valid state
				Cash += amount;
				// invalid state
				Thread.Sleep(0);
				Receivables -= amount;
				// valid state

				if (!hasBadReturn)
				{
					hasBadReturn = true;
					return;
				}
			}
			//Monitor.Exit(lockObject);


			// not really helpful
			//Interlocked.Add(ref Cash, amount);
			//Thread.Sleep(0);
			//Interlocked.Add(ref Receivables, -amount);
		}

		public decimal NetWorth
		{
			get
			{
				Monitor.Enter(lockObject);
				try
				{
					return Cash + Receivables;
				}
				finally
				{
					Monitor.Exit(lockObject);					
				}
			}
		}
	}

}
