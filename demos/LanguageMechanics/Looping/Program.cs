using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Looping
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = Enumerable.Range(0, 100000000).ToList();

            Stopwatch timer = Stopwatch.StartNew();
            int total = 0;

            //for (int i = 0; i < values.Length; i++)
            //{
            //    total += values[i];
            //}
            IEnumerator<int> iter = values.GetEnumerator();

            while (iter.MoveNext())
            {
             //   int value = (int)iter.Current;
                total += iter.Current;
            }
            //foreach (int value in values)
            //{
            //    total += value;
            //}

            timer.Stop();

            Console.WriteLine("Total elapsed {0}" ,  timer.Elapsed);

        }

    
    }
}
