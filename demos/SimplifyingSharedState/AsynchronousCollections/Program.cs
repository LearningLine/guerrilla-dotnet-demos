using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCollections
{
    

    class Program
    {
        private static void Main(string[] args)
        {

            Lazy<int> lazyVal = new Lazy<int>(() =>
            {
                Thread.Sleep(5000);
                return 42;
            });


            WaitForIt(lazyVal);
            Console.WriteLine("back in main");

            Console.ReadLine();
        }

        private static void WaitForIt(Lazy<int> lazyVal)
        {
            Console.WriteLine("Waiting..");

            int result =  lazyVal.Value;

            Console.WriteLine("Result {0}", result);
        }
    }
}

