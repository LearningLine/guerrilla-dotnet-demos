using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDictioanaryPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfValues = 10000000;

            
            var numbers = new ConcurrentDictionary<int, string>(8, numberOfValues);

            Stopwatch timer = Stopwatch.StartNew();
            Parallel.For(0, numberOfValues, i =>
            {
                numbers.TryAdd(i, i.ToString()); 
            });
            Console.WriteLine(timer.Elapsed);

        }
    }
}
