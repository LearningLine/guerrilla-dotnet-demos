
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<int> queue = new BlockingCollection<int>();

            int nConsumers = 4;
            for (int nConsumer = 0; nConsumer < nConsumers; nConsumer++)
            {
                Task.Factory.StartNew(() => Consumer(queue));

            }

            Producer(queue);
        }

        private static void Consumer(BlockingCollection<int> queue)
        {
            foreach (int item in queue.GetConsumingEnumerable())
            {
                Console.WriteLine("{0} : Consuming {1}",Task.CurrentId,item);
                Thread.Sleep(1000);
                Console.WriteLine("{0} :Done",Task.CurrentId);
            }
        }

        private static void Producer(BlockingCollection<int> queue)
        {
            while (true)
            {
                Thread.Sleep(250);
               
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.P:
                            queue.Add(new Random().Next(1, 100));
                            break;

                        case ConsoleKey.T:
                        {
                            PrintThreadPoolUsage("While");
                        }
                            break;
                    }
                }
            }
        }


        public static void PrintThreadPoolUsage(string label)
        {
            int cpu;
            int io;

            ThreadPool.GetAvailableThreads(out cpu, out io);

            Console.WriteLine("{0}: CPU {1} , IO : {2}",label,cpu,io);
        }
    }
}
