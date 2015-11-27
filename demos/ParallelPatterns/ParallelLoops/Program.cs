using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace CalculatingPi
{
    class Program
    {
        private const int PI_ITERATIONS = 200000000;

        static void Main(string[] args)
        {
            //CalculatePi(SequentialPi);

           
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

        // Pi = 4 * ( 1 - 1/3 + 1/5 - 1/7 + 1/11 ....  )
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

    }
       
}
