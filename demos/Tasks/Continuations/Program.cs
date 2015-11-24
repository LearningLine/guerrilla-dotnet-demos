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
            Task<int> t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task one starting");
                Thread.Sleep(2000);
                Console.WriteLine("Task one done");
                throw new Exception("BOOM!!");
                return 42;

            });

            t.ContinueWith(prevTask => // prevTask == t
            {
                Console.WriteLine("Task two starting, t.Result {0}",
                    prevTask.Result);
            },TaskContinuationOptions.OnlyOnRanToCompletion);

            t.ContinueWith(prevTask => // prevTask == t
            {
                Console.WriteLine("Task three starting, {0}",
                    prevTask.Exception.InnerExceptions.First().Message);
            },TaskContinuationOptions.OnlyOnFaulted);

            Console.ReadLine();
        }
    }
}
