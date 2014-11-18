using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousMethods
{
    interface IFibHandler
    {
        void HandleFib(int fib);
    }

    class FibHandler : IFibHandler
    {
        public void HandleFib(int fib)
        {
            Console.WriteLine(fib);
        }
    }

    public delegate void FibDelegate(int fib); // System.MulticastDelegate

    class Program
    {
        static void Main(string[] args)
        {
            //FibDelegate del = delegate(int fib)
            //{
            //    Console.Beep(2000 + fib * 2, 500);

            //};

            double prev = 1;
            GenerateFibs(20, fib =>
            {
                double ratio = fib/prev;

                Console.WriteLine(ratio);

                prev = fib; 
            });
        }

        static void PlayFib(int gib)
        {
            Console.Beep(2000 + gib * 5, 500);
        }

        public static void ShowFib(int fib)
        {
            Console.WriteLine(fib);
        }

        private static void GenerateFibs(int nFibs, FibDelegate handler)
        {
            int current = 1;
            int prev = 0;
            int prevprev = 0;

            for (int i = 0; i < nFibs; i++)
            {
                handler(current);
                prevprev = prev;
                prev = current;
                current = prev + prevprev;
            }
        }

  
    }
}
