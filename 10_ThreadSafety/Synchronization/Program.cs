using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Synchronization
{
    class Program
    {
        private const int N_ITERATIONS = 100000;

        static void Main(string[] args)
        {
            Console.WriteLine("press enter to start");
            Console.ReadLine();

            Counter[] counters = new Counter[] { 
                                                
                                                    new Counter(),
                                                    new InterlockedCounter(), 
                                                    new NonSharedCounter(), 
                                                    new MonitorCounter(), 
                                                    new SpinLockCounter(), 
                                                    new MutexCounter(), 
                                                };
            const int nThreads = 2;

            foreach (Counter counter in counters)
            {
                Stopwatch timer = Stopwatch.StartNew();

                ExerciseCounter(counter, nThreads);

                timer.Stop();

                Console.WriteLine("{0} : Result = {1} , Expected Result {2} , took {3} ms " , 
                    counter.GetType().Name ,
                    counter.Value ,
                    nThreads * N_ITERATIONS ,
                    timer.ElapsedMilliseconds );
            }
        }

        private static void ExerciseCounter(Counter counter, int nThreads)
        {
            Thread[] threads = new Thread[nThreads];

            for (int nThread = 0; nThread < threads.Length; nThread++)
            {
                threads[nThread] = new Thread(ExerciseThread);

                threads[nThread].Start(counter);
            }

            for (int nThread = 0; nThread < threads.Length; nThread++)
            {
                threads[nThread].Join();
            }
        }


        private static void ExerciseThread(object arg)
        {
            Console.WriteLine("Thread Id {0} running  " , Thread.CurrentThread.ManagedThreadId);

            Counter counter = (Counter)arg;

            for (int nIter = 0; nIter < N_ITERATIONS; nIter++)
            {
                counter.Increment();
            }
        }
    }
}
