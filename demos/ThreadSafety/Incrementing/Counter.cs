using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Incrementing
{
    public class Counter
    {
        protected int val;

        public virtual void Increment()
        {
            val++;
        }

        public virtual int Value { get { return val; } }
    }

    public class InterlockedCounter : Counter
    {
        public override void Increment()
        {
            Interlocked.Increment(ref val);
        }
    }

    public class LockFreeCounter : Counter
    {
        private int[] vals = new int[1000];

        public override void Increment()
        {
            vals[Thread.CurrentThread.ManagedThreadId * 32]++;
        }

        public override int Value { get { return vals.Sum(); } }
    }

    public class MutexCounter : Counter
    {
        private readonly Mutex mtx = new Mutex();

        public override void Increment()
        {
            mtx.WaitOne();
            try
            {
                base.Increment();
            }
            finally
            {
                mtx.ReleaseMutex();
            }
        }
    }

    public class MonitorCounter : Counter
    {
        object guard= new object();

        public override void Increment()
        {
            lock(guard)
            {
                base.Increment();
            }
        }
    }
}







