using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateTasks();

            //CreateTasksWithReturn();

            BadThreads();


            Console.WriteLine("Enter...");
            Console.ReadLine();
        }

        private static void BadThreads()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new ArgumentException("boo");
                return 42;
            });

            try
            {
                var r = t.Result;
            }
            catch (AggregateException ae)
            {
                
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    Console.WriteLine("Message: " + e.Message);
                }
            }
        }

        private static void CreateTasksWithReturn()
        {
            Task<int> t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting...");
                Thread.Sleep(1000);
                Console.WriteLine("done");
                return 42;
            });

            Console.WriteLine("The task has now started...");

            if (t.Wait(2000))
                Console.WriteLine("Your magic number is : {0}", t.Result);
            else
                Console.WriteLine("Oops, time out!");
        }

        private static void CreateTasks()
        {
            PrintTheadPoolStats();
            Task t = null;
            //t = new Task(() =>
            //{
            //    Console.WriteLine("Hello from task! {0}", t.Status);
            //   // t.Wait();
            //} );
            // //This runs on a background thread 
            //t.Start();

            t = Task.Factory.StartNew(() => { Console.WriteLine("Hello from task factory! {0}", t.Status); },
                TaskCreationOptions.LongRunning);
            // TaskCreationOptions.LongRunning -> new Thread()

            PrintTheadPoolStats();
        }

        private static void PrintTheadPoolStats()
        {
            int worker, io;
            ThreadPool.GetAvailableThreads(out worker, out io);
            Console.WriteLine("Workers: {0}, IO: {1}", worker, io);
        }
    }
}
