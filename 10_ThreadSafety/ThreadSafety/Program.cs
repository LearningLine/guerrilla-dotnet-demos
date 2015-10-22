using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadSafety
{
    class Program
    {
	    private const int NumberOfIterations = 10000000;// - for Mutex

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start.");
            Console.ReadKey();

            ICounter[] counters = new ICounter[]
            {
                new Counter(),
                new InterlockedCounter(),
                new MonitorCounter(),
				new AllocatedLockCounter(), 
				//new MutexCounter(), 
            };

            const int numberOfThreads = 2;

			foreach (ICounter counter in counters)
            {
                Stopwatch timer = Stopwatch.StartNew();
                ExerciseCounter(counter, numberOfThreads);
                timer.Stop();

                Console.WriteLine("{0}: Result={1}, Expected={2}, took {3}ms",
                    counter.GetType().Name,
                    counter.Value,
                    numberOfThreads * NumberOfIterations,
                    timer.ElapsedMilliseconds);
            }
        }
        private static void ExerciseCounter(ICounter counter, int numberOfThreads)
        {
            Thread[] threads = new Thread[numberOfThreads];
            for (int index = 0; index < threads.Length; index++)
            {
                threads[index] = new Thread(CountingThread);
                threads[index].Start(counter);
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        private static void CountingThread(object arg)
        {
            Console.WriteLine("Thread Id {0} running", Thread.CurrentThread.ManagedThreadId);
            ICounter counter = (ICounter)arg;

            for (int nIter = 0; nIter < NumberOfIterations; nIter++)
            {
                counter.Increment();
            }
        }
    }
}
