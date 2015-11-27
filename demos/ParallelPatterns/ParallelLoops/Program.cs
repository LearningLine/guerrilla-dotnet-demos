using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ParallelUtil;


namespace CalculatingPi
{
    class Program
    {
        private const int PI_ITERATIONS = 200000000;

        static void Main(string[] args)
        {
            CalculatePi(ParallelPi);
            CalculatePi(OptimusPi);
            CalculatePi(SequentialPi);

            CalculatePi(ParallelPi);
            CalculatePi(OptimusPi);
            CalculatePi(SequentialPi);

            // for (int i = 0; i < 20; i++)
            //Parallel.For(0, 20, new ParallelOptions { MaxDegreeOfParallelism = 2 }, 
            //    i =>
            //    {
            //        Console.WriteLine("{0}: {1}", Task.CurrentId, i);
            //    });
        }  

        private static void CalculatePi(Func<double> calcMethod)
        {
            Stopwatch timer = Stopwatch.StartNew();
            double pi = calcMethod();
            timer.Stop();
            Console.WriteLine("{0} took {1} ms to calculate {2}",
                calcMethod.Method.Name,
                timer.ElapsedMilliseconds,
                pi);
        }

        // Pi = 4 * ( 1 - 1/3 + 1/5 - 1/7 + 1/9 ....  )
        private static double SequentialPi()
        {
            double pi = 1;
            double multiplier = -1;

            for (int i = 3; i < PI_ITERATIONS; i += 2)
            {
                pi += multiplier * (1.0 / (double)i);
                multiplier = multiplier * -1;
            }

            pi = pi * 4.0;

            return pi;
        }

        private static double HorribleParallelPi()
        {
            double pi = 1;
            double multiplier = -1;

            
          //  for (int i = 3; i < PI_ITERATIONS; i += 2)
            Parallel.For(0, (PI_ITERATIONS - 3)/2, loopIndex =>
            {
                int i = loopIndex*2 + 3;
                pi += multiplier*(1.0/(double) i);
                multiplier = multiplier*-1;
            });

            pi = pi * 4.0;

            return pi;
        }

        private static double ParallelPi()
        {
            double pi = 1;


            object piGuard = new object();
            //  for (int i = 3; i < PI_ITERATIONS; i += 2)
            Parallel.For(0, (PI_ITERATIONS - 3)/2,
                () => 0.0,
                (loopIndex, loopState, localState) =>
                {
                    double multiplier = loopIndex % 2 == 0 ? -1 : 1;
                    int i = loopIndex*2 + 3;
                    localState += multiplier*(1.0/(double) i);

                    return localState;
                },
                localState =>
                {
                    lock (piGuard)
                    {
                        pi += localState;
                    }
                });

            pi = pi * 4.0;

            return pi;
        }

        // Pi = 4 * ( 1 - 1/3 + 1/5 - 1/7 + 1/9 ....  )
        private static double OptimusPi()
        {
            double pi = 1;

            var rng = new Range {Start = 3, End = PI_ITERATIONS - 1};

            object guard = new object();
            Parallel.ForEach(rng.CreateSubRanges(8),
                () => 0.0,
                (loopRange, loopState, localState) =>
                {
                    double multiplier = -1;

                    for (int i = loopRange.Start; i <= loopRange.End; i += 2)
                    {
                        localState += multiplier*(1.0/(double) i);
                        multiplier = multiplier*-1;
                    }

                    return localState;
                },
                localState =>
                {
                    lock (guard)
                    {
                        pi += localState;
                    }
                });

            pi = pi * 4.0;

            return pi;
        }

    }
       
}
