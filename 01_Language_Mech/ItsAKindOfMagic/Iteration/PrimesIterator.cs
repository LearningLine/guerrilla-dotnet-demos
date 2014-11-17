using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iteration
{
    public class PrimesEnumerable : IEnumerable<int>
    {
        int maxNumberOfPrimes = 0;

        public PrimesEnumerable(int nPrimes)
        {
            maxNumberOfPrimes = nPrimes;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new PrimesEnumerator(maxNumberOfPrimes);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PrimesEnumerator : IEnumerator<int>
    {
        int[] previousPrimes;

        int nNextPrime = 0;
       

        public PrimesEnumerator(int maxNumberOfPrimes)
        {
            previousPrimes = new int[maxNumberOfPrimes];

            previousPrimes[0] = 2;
            previousPrimes[1] = 3;

        }

        private int current;
        public int Current
        {
            get { return current; }
        }

        public void Dispose()
        {
            return;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {

            if (nNextPrime >= previousPrimes.Length) return false;

            if (nNextPrime == 0)
            {
                current = 2;
                nNextPrime++;
                return true;
            }

            if (nNextPrime == 1)
            {
                current = 3;
                nNextPrime++;
                return true;

            }

            int nextValueToTry = previousPrimes[nNextPrime - 1] + 2;

            do
            {
                bool isPrime = true;

                for (int primeToTry = 0; primeToTry < nNextPrime && isPrime; primeToTry++)
                {
                    isPrime &= !(nextValueToTry % previousPrimes[primeToTry] == 0);
                }

                if (isPrime)
                {
                    previousPrimes[nNextPrime++] = nextValueToTry;
                    current = nextValueToTry;
                    return true;
                }

                nextValueToTry += 2;
            }
            while (nextValueToTry < int.MaxValue);

            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

}
