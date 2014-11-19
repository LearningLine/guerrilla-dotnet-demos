using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chaining
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 1 starting ...");
                Thread.Sleep(2500);
                Console.WriteLine("Task 1 finishing");
                throw new Exception("Ouch");

                return 42;
            });

            t1.ContinueWith(tPrevious =>
            {
                Console.WriteLine("Task 2 now running! And the starting data is {0}", tPrevious.Result);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            t1.ContinueWith(tPrevious =>
            {
                string msg = tPrevious.Exception.InnerExceptions.First().Message;
                Console.WriteLine("Task 3 now running (cleaning up t 1's mess: {0})", msg);
            }, TaskContinuationOptions.OnlyOnFaulted);


            Console.ReadLine();
        }
    }
}