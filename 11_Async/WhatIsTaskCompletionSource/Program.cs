using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhatIsTaskCompletionSource
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = CreateForegroundTask(() =>
            {
                Console.WriteLine("Wake me when it's over...");
                Thread.Sleep(5000);
                Console.WriteLine("Good morning.");
            });

            Console.WriteLine("Waiting around...");
           // t.Wait();
           // Console.WriteLine("Done");
        }

        private static Task CreateForegroundTask(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(_ =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception x)
                {
                    tcs.SetException(x);
                }
            });
            thread.Start();

            return tcs.Task;
        }
    }
}
