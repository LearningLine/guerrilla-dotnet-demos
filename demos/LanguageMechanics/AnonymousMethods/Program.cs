using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AnonymousMethods
{
    class Clojure
    {
        public int total;
        public void Add(int val)
        {
            total += val;
        }
    }
  

    class Program
    {
        static void Main(string[] args)
        {
            List<int> ints = new List<int>();
            ints.Add(1);
            ints.Add(17);
            ints.Add(42);
            ints.Add(5);
            ints.Add(99);
            ints.Add(74);
            ints.Add(100);
            ints.Add(7);

            ints.RemoveAll(val => val < 12);

            
            //foreach (int i in ints)
            //{
            //    if (i < 12)
            //    {
            //        ints.Remove(i);
            //    }
            //}

            foreach (int i in ints)
            {
                Console.WriteLine(i);
            }

        }

        private static void ClosureMechanics(List<int> ints)
        {
            Clojure closure = new Clojure();

            closure.total = 0;

            ints.ForEach(closure.Add);

            Console.WriteLine(closure.total);

            //int total = 0; // this is on the heap

            //ints.ForEach(delegate(int val)
            //{
            //    total += val;
            //});

            //   Console.WriteLine(total);
        }

        private static bool FootballNumbers(int number)
        {
            return number < 12;
        }
    }
}
