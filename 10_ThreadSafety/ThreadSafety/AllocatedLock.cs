using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ThreadSafety
{
	public class AllocatedLock : IDisposable
	{
		private AllocatedLock _toLock;

		public AllocatedLock()
		{
			
		}

		public AllocatedLock(AllocatedLock toLock)
		{
			_toLock = toLock;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLock(AllocatedLock toLock)
		{
			_toLock = toLock;
		}

		public void Dispose()
		{
			Monitor.Exit(_toLock);
		}
	}
}