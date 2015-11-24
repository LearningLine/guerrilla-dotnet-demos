using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreatingTasks();
            // Future<int> 
            Task<int> future = Task.Factory.StartNew(() =>
            {
                throw new Exception("BANG!");
                Thread.Sleep(1000);
                return 42;
            });

            // throws NotReadyComeBackLaterException ?
            // returns default value , 0 ?
            // Waits until completed ? YES
            //Console.WriteLine(future.Result);

            //if (future.Wait(TimeSpan.FromSeconds(2)))
            //{
            //    Console.WriteLine(future.Result);
            //}
            //else
            //{
            //    Console.WriteLine("Timedout !!!");
            //}

            try
            {
                Console.WriteLine(future.Result);
            }
            catch (AggregateException errors)
            {
                foreach (Exception error in errors.Flatten().InnerExceptions)
                {
                    Console.WriteLine(error.Message);
                    
                }
            }


            Console.ReadLine();
        }

        private static void CreatingTasks()
        {
            PrintThreadPoolUsage();
            Task t = null;

            //t = new Task(() =>
            //{
            //    PrintThreadPoolUsage();
            //    Console.WriteLine("Hello Parallel World!!!");
            //    Console.WriteLine("This is a background thread ? {0}",
            //        Thread.CurrentThread.IsBackground);
            //});

            //t.Start();

            t = Task.Factory.StartNew(() =>
            {
                PrintThreadPoolUsage();
                Console.WriteLine("Hello Parallel World!!!");

                // Danager could access t before being set
                Console.WriteLine(t.Status);
            }, TaskCreationOptions.LongRunning);

            //t = Task.Run(() =>
            //{
            //    PrintThreadPoolUsage();
            //    Console.WriteLine("Hello Parallel World!!!");
            //});
        }

        private static void PrintThreadPoolUsage()
        {
            int cpu;
            int io;

            ThreadPool.GetAvailableThreads(out cpu, out io);

            Console.WriteLine("CPU : {0} IO : {1}",
                cpu,io);
        }
    }
}









