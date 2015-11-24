using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // responsible for Orchestrating Cancellation
            CancellationTokenSource cts = new CancellationTokenSource();

            // CancellationToken allows to observe cancellation

            cts.CancelAfter(TimeSpan.FromSeconds(4));

            for (int i = 0; i < 100; i++)
           //foreach(int i in Enumerable.Range(0,100))
            {
                // Change the point of capture
                // creates 100 Closures
                int locali = i;
                Task.Factory.StartNew(() =>
                {
                    cts.Token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                    cts.Token.ThrowIfCancellationRequested();
                    Console.WriteLine(locali);

                    // Would create one closure
                    // Console.WriteLine(i);
                },cts.Token);
                //,TaskCreationOptions.LongRunning);
              
            }
            Console.WriteLine("Press enter to cancel");
            Console.ReadLine();
            cts.Cancel();
            Console.WriteLine("Press enter to force exit");
            Console.ReadLine();
        }
    }
}
