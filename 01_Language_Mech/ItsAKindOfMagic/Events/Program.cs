using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var clock = new Clock();

           clock.Tick += PrintDot;

       
            Task.Factory.StartNew(clock.Run);

            Console.ReadLine();
        }

        private static void PrintDot(object sender, EventArgs e)
        {
            Console.Write(".");
        }

       
    }
}
