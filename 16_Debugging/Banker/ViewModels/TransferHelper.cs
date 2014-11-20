using System;
using System.Threading;
namespace WpfBankerSimulation.ViewModels
{
    partial class AccountViewModel
    {
        private readonly Mutex _guard = new Mutex();

        internal static void TransferAmountBetweenAccounts(
            AccountViewModel source, 
            AccountViewModel dest, 
            int value)
        {
			lock(source)
				lock (dest)
				{
					if (source.Balance > value)
					{
						source.Balance -= value;
						dest.Balance += value;
					}
				}
			//WaitHandle.WaitAll(new WaitHandle[] { source._guard, dest._guard });
			//try
			//{
			//	// Do not perform transfer
			//	if (source.Balance > value)
			//	{
			//		source.Balance -= value;
			//		dest.Balance += value;
			//	}
			//	//RunDelay(40);
			//}
			//finally
			//{
			//	source._guard.ReleaseMutex();
			//	dest._guard.ReleaseMutex();
			//}
        }

        private static void RunDelay(int seconds)
        {
            int x = 0;
            var when = DateTime.Now + TimeSpan.FromMilliseconds(seconds);
            while (DateTime.Now < when)
            {
                unchecked { x++; } 
            }
        }
    }
}
