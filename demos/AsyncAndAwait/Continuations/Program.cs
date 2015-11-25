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

            //Task<int> result = Add(40, 2);

            //Console.WriteLine(result.Status);
            //Console.WriteLine(result.Result);

            DisplayClockMechanics();

            while (true)
            {
                PrintThreadPoolUsage("While");
                Console.ReadLine();
            }
        }

        public static async Task<int> Add(int x, int y)
        {
            return x + y;
        }

        public static async Task DisplayClock()
        {
            Console.WriteLine("Running clock");
           while (true)
           {
              // await Task.Delay(500); // GetAwaiter
               Console.WriteLine("Tick");
         //      PrintThreadPoolUsage("Tick");
          //      await Task.Delay(500);
               Console.WriteLine("Tock");
           }
        }

        public static void DisplayClockMechanics()
        {
            new DisplayClockStateMachine().MoveNext();
        }
      
        private static void PrintThreadPoolUsage(string label)
        {
            int cpuThreads = 0;
            int ioThreads = 0;

            ThreadPool.GetAvailableThreads(out cpuThreads, out ioThreads);
            Console.WriteLine("{0} : CPU = {1} , IO = {2}", label, cpuThreads, ioThreads);
        }

    }

    internal class DisplayClockStateMachine
    {
        private int state = 1;

        public void MoveNext()
        {
            start:
            switch (state)
            {
                case 1:
                    Console.WriteLine("Running clock");
                    state = 2;
                    goto start;

                case 2:
                    var awaiter1 = Task.Delay(500).GetAwaiter();
                    state = 3;
                    if (awaiter1.IsCompleted)
                    {
                        goto start;
                    }
                    awaiter1.OnCompleted(MoveNext);
                    break;

                case 3:
                    Console.WriteLine("Tick");
                    var awaiter2 = Task.Delay(500).GetAwaiter();
                    state = 4;
                    if (awaiter2.IsCompleted)
                    {
                        goto start;
                    }
                    awaiter2.OnCompleted(MoveNext);
                    break;

                case 4:
                    Console.WriteLine("Tock");
                    state = 2;
                    goto start;
            }
        }
    }
}
