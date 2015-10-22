using System;
using Microsoft.Owin.Hosting;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.WriteLine("Press [Enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
