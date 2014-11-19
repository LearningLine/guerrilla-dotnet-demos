using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cancelling
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new CancellationTokenSource();

            //foreach (var i in Enumerable.Range(0, 100))
            for (int i = 0; i < 100; i++)
            {
                int localI = i;
                Task.Factory.StartNew(() =>
                {
                    //if (source.Token.IsCancellationRequested)
                    //    return;
                    source.Token.ThrowIfCancellationRequested();

                    Thread.Sleep(1000);

                    source.Token.ThrowIfCancellationRequested();
                    
                    Console.WriteLine(localI);
                }, source.Token);
            }

            Console.WriteLine("Press enter to cancel");
            Console.ReadLine();

            source.Cancel();

            Console.WriteLine("Done");
            Console.ReadLine();

        }
    }
}
