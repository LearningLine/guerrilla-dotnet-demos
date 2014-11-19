using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GetResult();
            Console.WriteLine("Getting result...");
            Console.ReadLine();
        }

        private async static void GetResult()
        {
            var proxy = new Proxy.DoClient();
            var res = await proxy.DoThisAsync();
            Console.WriteLine(res);
        }
    }
}
