using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Looping
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = CreateValues(200000000);


            for (int i = 0; i < 2; i++)
            {
                ForeachArrayOfInts(values);
               
                Console.WriteLine();
            }

        }

        private static void ForeachArrayOfInts(int[] values)
        {
            Stopwatch timer = Stopwatch.StartNew();
            timer.Stop();
            timer.Start();
            int total = 0;

            foreach (int value in values)
            {
                total += value;
            }

            timer.Stop();

            Console.WriteLine("Total elapsed {0}", timer.Elapsed);
        }

        private static int[] CreateValues(int nValues)
        {
            int[] vals = new int[nValues];

            return vals;

        }
    }
}
