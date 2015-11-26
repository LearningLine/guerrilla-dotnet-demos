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

            Random rnd = new Random();
            
            Console.WriteLine("Enter to start, be calm no hurry");
            Console.ReadLine();
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 10000000; i++)
            {
                int val;

                if (rnd.NextDouble() < 0.05)
                {
                    int.TryParse(number,out val);
                }
                else
                {
                    int.TryParse("2",out val);
                }

                //try
                //{
                //    if (rnd.NextDouble() < 0.05)
                //    {
                //        int.Parse(number);
                //    }
                //    else
                //    {
                //        int.Parse("2");
                //    }
                //}
                //catch (Exception error)
                //{
                //    // nothing to see here
                //}
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
