using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnythingATask
{
    class Program
    {
        static void Main(string[] args)
        {
            Task fgTask = CreateForegroundTask(() =>
            {
                Console.WriteLine("Foreground thread starting");
                Console.WriteLine("Is Foreground? {0}",
                    !Thread.CurrentThread.IsBackground);
                Thread.Sleep(2000);
                Console.WriteLine("Foreground thread done");
            });

            fgTask.Wait();
        }

        private static Task CreateForegroundTask(Action action)
        {
            TaskCompletionSource<object> tcs = 
                new TaskCompletionSource<object>();

            // Creates a fg task
            Thread fred = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception error)
                {
                    tcs.SetException(error);
                }

            });

            fred.Start();

            //usedful for testing stubs
            //return Task.FromResult<object>(null);


            return tcs.Task;
        }
    }
}
