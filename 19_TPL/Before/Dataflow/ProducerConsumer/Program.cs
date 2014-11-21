using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ProducerConsumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // OLD_Main();
            //ProdConsumerMethod();

            var ab1 = new ActionBlock<int>(n =>
            {
                Console.WriteLine("Block one consuming: " + n);
                Thread.Sleep(new Random().Next(2000));
                Console.WriteLine("Block one done.");
            }, new ExecutionDataflowBlockOptions(){BoundedCapacity = 1});
            var ab2 = new ActionBlock<int>(n =>
            {
                Console.WriteLine("Block two consuming: " + n);
                Thread.Sleep(new Random().Next(2000));
                Console.WriteLine("Block two done.");
            }, new ExecutionDataflowBlockOptions() { BoundedCapacity = 1 });

            var bblock = new BufferBlock<int>();

            bblock.LinkTo(ab1);
            bblock.LinkTo(ab2);

            while (true)
            {
                bblock.Post(new Random().Next());
                Console.ReadLine();
            }

        }

        private static void ProdConsumerMethod()
        {
            var ab = new ActionBlock<int>(item =>
            {
                Console.WriteLine("{0} : Consuming {1}", Task.CurrentId, item);
                Thread.Sleep(1000);
                Console.WriteLine("{0} : Done", Task.CurrentId);
            });


            bool quit = false;

            while (!quit)
            {
                Thread.Sleep(250);

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.P:
                            ab.Post(new Random().Next(1, 100));
                            //queue.Add(new Random().Next(1, 100));
                            break;

                        case ConsoleKey.T:
                        {
                            PrintThreadPoolUsage("While");
                        }
                            break;
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                    }
                }
            }

            Console.WriteLine("Producer done!");
            ab.Complete();
            ab.Completion.Wait();
            Console.WriteLine("Exiting process cleanly...");
        }

        private static void OLD_Main()
        {
            var queue = new BlockingCollection<int>();

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
                Console.WriteLine("{0} : Consuming {1}", Task.CurrentId, item);
                Thread.Sleep(1000);
                Console.WriteLine("{0} :Done", Task.CurrentId);
            }
        }

        private static void Producer(BlockingCollection<int> queue)
        {
            while (true)
            {
                Thread.Sleep(250);

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

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

            Console.WriteLine("{0}: CPU {1} , IO : {2}", label, cpu, io);
        }
    }
}