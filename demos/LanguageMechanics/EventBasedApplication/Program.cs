using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EventBasedApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();

            Thread clockTask = new Thread(clock.Run);
            clockTask.IsBackground = true;

            clockTask.Start();

            //clock.Tick += new EventHandler<EventArgs>(Tick);
            
            Console.ReadLine();
        }

        static void Tick(object sender, EventArgs e)
        {
            Console.Write(".");
        }
    }
}
