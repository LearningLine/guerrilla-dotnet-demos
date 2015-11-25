using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Proxies;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    DoClient proxy = new DoClient();

                    int val = proxy.DoThis();

                    Console.WriteLine(val);
                });
            }

            Console.ReadLine();
        }
    }
}
