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


            foreach (int prime in GetPrimes(100000))
            {
                Console.WriteLine(prime);
            }
          

          
        }

       
        public static IEnumerable<int> GetPrimes(int nValues )
        {
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
    }
}
