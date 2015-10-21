using System;
using System.Linq;
using System.Net;
using System.Threading;

namespace ThreadSafety
{

	internal class Counter : ICounter
	{
		private int value;

		public int Value
		{
			get { return value; }
		}

		public void Increment()
		{
			value++;
		}
	}

	internal class InterlockedCounter : ICounter
	{
		private int value;

		public int Value
		{
			get { return value; }
		}

		public void Increment()
		{
			Interlocked.Increment(ref value);
		}
	}

	internal class MonitorCounter : ICounter
	{
		private int value;
		private object lockObj = new object();

		public int Value
		{
			get
			{
				lock (lockObj)
				{
					return value;
				}
			}
		}

		public void Increment()
		{
			Monitor.Enter(lockObj);
			try
			{
				value++;
			}
			finally
			{
				Monitor.Exit(lockObj);
			}
		}
	}

	internal class AllocatedLockCounter : ICounter
	{
		private int value;
		private AllocatedLock lockObj = new AllocatedLock();

		public int Value
		{
			get
			{
				using (lockObj.Lock(TimeSpan.FromMinutes(1)))
				{
					return value;
				}
			}
		}

		public void Increment()
		{
			using (lockObj.Lock(TimeSpan.FromMinutes(1)))
			{
				value++;
			}
		}
	}

	internal class MutexCounter : ICounter
	{
		private int value;
		Mutex mutex = new Mutex();
		public int Value
		{
			get { return value; }
		}

		public void Increment()
		{
			mutex.WaitOne();
			try
			{
				value++;
			}
			finally
			{
				mutex.ReleaseMutex();
			}

		}
	}
}