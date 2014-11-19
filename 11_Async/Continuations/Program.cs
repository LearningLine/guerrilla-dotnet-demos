using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Continuations
{


    class Program
    {
        static void Main(string[] args)
        {
            PrintThreadPoolUsage("Main");

            Task.Run(() => DisplayClock());

           while (true)
           {
               PrintThreadPoolUsage("While");
               Console.ReadLine();
           }
        }

        public static  void DisplayClock()
        {
            Console.WriteLine("Running clock");
           while (true)
           {
               Thread.Sleep(500);
               Console.WriteLine("Tick");
               Thread.Sleep(500);
               Console.WriteLine("Tock");
           }
        }
      
        private static void PrintThreadPoolUsage(string label)
        {
            int cpuThreads = 0;
            int ioThreads = 0;

            ThreadPool.GetAvailableThreads(out cpuThreads, out ioThreads);
            Console.WriteLine("{0} : CPU = {1} , IO = {2}", label, cpuThreads, ioThreads);
        }

    }
}
