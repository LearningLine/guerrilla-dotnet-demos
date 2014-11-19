using System;
using System.Threading;

namespace InterTransfer
{
    public struct LockHelper:IDisposable
    {
        private readonly object _monitorObject;

        public LockHelper(object monitorObject)
        {
            _monitorObject = monitorObject;
        }


        public void Dispose()
        {
            Monitor.Exit(_monitorObject);
        }
    }
    public static class LockExtensions
    {
        public static LockHelper Lock(this object toLock, TimeSpan timeout)
        {
            if (!Monitor.TryEnter(toLock,timeout))
            {
                throw new TimeoutException("Failed to get lock");
            }
            
            // Stack Based instance of LockHelper
            // this is cool as its as big as a reference
            return new LockHelper(toLock);

        }
    }
}