using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Incrementing
{
    

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            Counter[] counters = new Counter[]
            {
                new Counter(), 
                new LockFreeCounter(),
                new InterlockedCounter(),
                new MonitorCounter(),
                new MutexCounter(), 
            };

            foreach (Counter counter in counters)
            {

                int nThreads = 2;
                int nIterations = 1000000;

                Stopwatch timer = Stopwatch.StartNew();
                ExerciseCounter(counter, nThreads, nIterations);

                Console.WriteLine("{2} Value = {0} Expected {1} - took {3}",
                    counter.Value,
                    nThreads*nIterations,
                    counter.GetType().Name,
                    timer.Elapsed);

            }
        }

        private static void ExerciseCounter(Counter counter,
            int nThreads,
            int nIncrements )
        {
            Task[] threads = new Task[nThreads];

            for (int nThread = 0; nThread < threads.Length; nThread++)
            {
               threads[nThread] = Task.Factory.StartNew( () =>
                    {
                        for (int nIteration = 0; nIteration < nIncrements; nIteration++)
                        {
                            counter.Increment();
                        }
                    }
                   );
            }

            Task.WaitAll(threads);
        }
    }
}
