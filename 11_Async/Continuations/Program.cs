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

            DisplayClockMechanics();
            //DisplayClock();
            //Task.Run(() => DisplayClock());

           while (true)
           {
               PrintThreadPoolUsage("While");
               Console.ReadLine();
           }
        }

        public static void DisplayClockMechanics()
        {
            new DisplayClockStateMachine().MoveNext();
        }

        public async static void DisplayClock()
        {
            Console.WriteLine("Running clock");
           while (true)
           {
               //Thread.Sleep(500);
               await Task.Delay(500);
               Console.WriteLine("Tick");
               //Thread.Sleep(500);
               await Task.Delay(500);
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

    internal class DisplayClockStateMachine
    {

        private int state = 0;
        public void MoveNext()
        {
            start:

            switch (state)
            {
                case 0:
                {
                    Console.WriteLine("Running clock");
                    state = 1;
                    goto start;
                }
                case 1:
                {
                    var awaiter = Task.Delay(500).GetAwaiter();
                    state = 2;
                    if (awaiter.IsCompleted)
                    {
                        goto start;
                    }
                    awaiter.OnCompleted(MoveNext);
                }
                break;

                case 2:
                {
                    Console.WriteLine("Tick");
                    var awaiter = Task.Delay(500).GetAwaiter();
                    state = 3;
                    if (awaiter.IsCompleted)
                    {
                        goto start;
                    }
                    awaiter.OnCompleted(MoveNext);
                }
                    break;

                case 3:
                    {
                        Console.WriteLine("Tock");
                        state = 1;
                       goto start;
                    }
                    break;
            }
        }
    }
}










