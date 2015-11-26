using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<int> queue = 
                new BlockingCollection<int>(new ConcurrentQueue<int>());

            Task[] consumers = new Task[4];

            for (int i = 0; i < consumers.Length; i++)
            {
                consumers[i] = Task.Run(() => Consume(queue));
            }

            var rnd = new Random();
            bool quit = false;
            while (!quit)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.P:
                        queue.Add(rnd.Next(2000,5000));
                        break;

                    case ConsoleKey.Q:
                        queue.CompleteAdding();
                        quit = true;
                        break;
                }
            }

            Task.WaitAll(consumers);
            Console.WriteLine("All done");

        }

        private static void Consume(BlockingCollection<int> queue)
        {
            foreach (int val in queue.GetConsumingEnumerable())
            {
                Console.WriteLine("{0} consuming {1}", Task.CurrentId, val);
                Thread.Sleep(val);
                Console.WriteLine("{0} ready ...", Task.CurrentId);
            }

            Console.WriteLine("{0} done", Task.CurrentId);

            //while (true)
            //{
            //    int val = queue.Take();
            //    Console.WriteLine("{0} consuming {1}", Task.CurrentId, val);
            //    Thread.Sleep(val);
            //    Console.WriteLine("{0} ready ...", Task.CurrentId);
            //}
        }
        private static void Consume(ConcurrentQueue<int> queue)
        {
            while (true)
            {
                int val;
                if (queue.TryDequeue(out val))
                {
                    Console.WriteLine("{0} consuming {1}", Task.CurrentId, val);
                    Thread.Sleep(val);
                    Console.WriteLine("{0} ready ...", Task.CurrentId);
                }
            }
        }
    }
}
