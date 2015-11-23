using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Iteration
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (int i in Count())
            {
                Console.WriteLine(i);
            }
            //foreach (int prime in GetPrimes(100000))
            //{
            //    Console.WriteLine(prime);
            //}


        }

        private static IEnumerable<int> Count()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }


        public static IEnumerable<int> GetPrimes(int nValues )
        {
          // return new PrimesEnumerable(nValues);
            int[] primes = new int[nValues];

            primes[0] = 2;
            int nPrime = 1;

            int nextValueToTry = 3;
            while( nPrime < nValues )
            {
                bool isPrime = true;

                for (int primeToTry = 0; primeToTry < nPrime && isPrime ; primeToTry++)
                {
                    isPrime &= !(nextValueToTry % primes[primeToTry] == 0);
                }

                if (isPrime)
                {
                   primes[nPrime++] = nextValueToTry;

                    yield return nextValueToTry;

                }
                nextValueToTry += 2;
            }

          // return primes;
        }
    }
}
