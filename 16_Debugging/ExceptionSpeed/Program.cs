using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string number = "one";

            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < 100000; i++)
            {
                // todo; parse some numbers.
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
