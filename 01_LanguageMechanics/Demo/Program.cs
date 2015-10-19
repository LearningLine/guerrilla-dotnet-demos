using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (int i in DivisibleBy(0, 50, 7))
            {
                Console.WriteLine(i);

            }

            List<int> primes = new List<int> { 3, 5, 7 };

            IEnumerable<int> seven = from prime in primes
                        where prime == 7
                        select prime;

            IEnumerable<int> seven2 = primes.Where(prime => prime == 7);
            

            primes.RemoveAll(prime => prime < 5);

            
            Console.ReadLine();
        }

        private static bool RemoveLessThan5(int obj)
        {
            return obj < 5;
        }

        private static IEnumerable<int> DivisibleBy(int min, int max, int divisor)
        {
            for (int i = min; i < max; i++)
                if (i % divisor == 0)
                    yield return i;
        }


    }
}
