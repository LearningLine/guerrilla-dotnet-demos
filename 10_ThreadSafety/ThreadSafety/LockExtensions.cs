using System;
using System.Threading;

namespace ThreadSafety
{
	public static class LockExtensions
	{
		public static AllocatedLock Lock(this AllocatedLock toLock, TimeSpan timeout)
		{
			if (Monitor.TryEnter(toLock, timeout))
			{
				toLock.SetLock(toLock);
                return toLock;
			}
			else
			{
				throw new TimeoutException(string.Format("Failed to get lock within {0}", timeout));
			}
		}


	}
}