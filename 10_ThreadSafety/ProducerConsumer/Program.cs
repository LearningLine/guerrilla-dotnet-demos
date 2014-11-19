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
            var foo = new BlockingCollection<int>(); // Default is to use a ConcurrentQueue<int>();
          

            Task[] consumerTasks = new Task[5];
            for (int i = 0; i < consumerTasks.Length; i++)
            {
                consumerTasks[i] = Task.Factory.StartNew(() => Consumer(foo));
            }

            Producer(foo);

            Task.WaitAll(consumerTasks);

        }

        private static void Consumer(BlockingCollection<int> foo)
        {

            // MoveNext blocks if no more items, unless completeAdding gets called
            // and which case returns false
            foreach(int val in foo.GetConsumingEnumerable())
            {
               Console.WriteLine("{0}:{1}",Thread.CurrentThread.ManagedThreadId,val); 
            }

            Console.WriteLine("Consumer {0} all done",Thread.CurrentThread.ManagedThreadId);
        }

        private static void Producer(BlockingCollection<int> foo)
        {
            Random rnd = new Random();
            bool shutdown = false;
            while (!shutdown)
            {
                foo.Add(rnd.Next(100));
                Thread.Sleep(rnd.Next(1000));

                shutdown = Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q;
            }
            // Signal no more new items will be entered into the collection
            foo.CompleteAdding();
            Console.WriteLine("Producer all done...");
        }
    

        private static void Consumer(ConcurrentQueue<int> foo)
        {

            // Busy Wait
            while (true)
            {
                int val;

                if (foo.TryDequeue(out val))
                {
                    Console.WriteLine("{0}:{1}",Thread.CurrentThread.ManagedThreadId,val);
                }
            }
        }

        private static void Producer(ConcurrentQueue<int> foo)
        {
            Random rnd = new Random();
            while (true)
            {
                foo.Enqueue(rnd.Next(100));
                Thread.Sleep(rnd.Next(1000));
            }
        }
    }
}








