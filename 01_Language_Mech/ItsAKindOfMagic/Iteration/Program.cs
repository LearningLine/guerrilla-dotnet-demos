using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iteration
{
    class Program
    {
        static void Main(string[] args)
        {


            //foreach (int prime in GetPrimesCodeGen(100000))
            //{
            //    Console.WriteLine(prime);
            //}

            foreach (int i in Count())
            {
                Console.WriteLine(i);
            }
          

          
        }

        private static IEnumerable<int> Count()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }


        public static IEnumerable<int> GetPrimes(int nValues )
        {
            return new PrimesEnumerable(nValues);
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
                }
                nextValueToTry += 2;
            }

            return primes;
        }

        public static IEnumerable<int> GetPrimesCodeGen(int nValues)
        {
            int[] primes = new int[nValues];

            yield return 2;
            primes[0] = 2;
            int nPrime = 1;

            int nextValueToTry = 3;
            while (nPrime < nValues)
            {
                bool isPrime = true;

                for (int primeToTry = 0; primeToTry < nPrime && isPrime; primeToTry++)
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

        }
    }
}
