using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Synchronization
{
    public class Counter
    {
        protected int val;

        public virtual void Increment()
        {
            val++;
        }

        public virtual int Value
        {
            get { return val; }
        }
    }

    public class InterlockedCounter : Counter
    {
        public override void Increment()
        {
            Interlocked.Increment(ref val);
        }
    }

    public class NonSharedCounter : Counter
    {
       // private int[] localValues = new int[10];
        private int[] localValues = new int[1000];

        public override void Increment()
        {
            // localValues[Thread.CurrentThread.ManagedThreadId]
            localValues[Thread.CurrentThread.ManagedThreadId * 100]++;
        }

        public override int Value
        {
            get { return localValues.Sum(); }
        }
    }


    public class SpinLock
    {
        private int locked = 0;
        public void Lock(int nAttempts)
        {
            int aquiredLock = 1;

            do
            {
                nAttempts--;
                aquiredLock = Interlocked.Exchange(ref locked, 1);
            } while (aquiredLock == 1 && nAttempts > 0);


            if (aquiredLock == 0) return;
            
            throw new Exception("Failed to get lock");
        }

        public void UnLock()
        {
            locked = 0;
        }
    }

    public class MutexCounter : Counter
    {
        // Kernel 
        private Mutex mutex = new Mutex();
        public override void Increment()
        {
            mutex.WaitOne();

            try
            {
                base.Increment();
            }
            finally
            {
                mutex.ReleaseMutex();
            }


        }
    }

    public class MonitorCounter : Counter
    {
        private object mutex = new object();
        public override void Increment()
        {
            lock (mutex)
            {
                base.Increment();
            }

        }
    }

    public class SpinLockCounter : Counter
    {
        private SpinLock spinLock = new SpinLock();
        public override void Increment()
        {
            spinLock.Lock(10000);
            try
            {
                base.Increment();
            }
            finally
            {
                spinLock.UnLock();
            }

        }
    }
}




















