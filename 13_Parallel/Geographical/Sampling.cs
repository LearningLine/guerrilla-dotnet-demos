using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geographical
{
    public static class Sampling
    {
        public static IEnumerable<T> RandomSample<T>(this IEnumerable<T> rows, int nSamples)
        {
            Random rnd = new Random();

            T[] sample = new T[nSamples];

            int nSamplesTaken = 0;

            foreach (T item in rows)
            {
                if (nSamplesTaken < sample.Length)
                {
                    sample[nSamplesTaken] = item;
                }
                else
                {
                    // As the amount of samples increases the probability
                    // of including a value gets less..due to the fact
                    // that it has a greater chance of surviving if it gets
                    // placed into the sample over earlier selections


                    if (rnd.Next(nSamplesTaken) < nSamples)
                    {
                        sample[rnd.Next(nSamples)] = item;
                    }
                }

                nSamplesTaken++;
            }

            if (nSamplesTaken >= nSamples)
            {
                return sample;
            }
            else
            {
                return sample.Take(nSamplesTaken);
            }
        }

        public static IEnumerable<T[]> HeapPermute<T>(this T[] items)
        {

            return HeapPermute((T[])items.Clone(), items.Length);
        }

        public static IEnumerable<T[]> HeapPermute<T>(this T[] items, int n)
        {
            if (n == 1)
            {
                yield return (T[])items.Clone();
            }
            else
            {
                for (int i = 0; i < n; i++)
                {

                    foreach (var perm in HeapPermute(items, n - 1))
                    {
                        yield return perm;
                    }

                    if (n % 2 == 1) // if n is odd
                    {
                        T temp = items[0];
                        items[0] = items[n - 1];
                        items[n - 1] = temp;

                        //   swap(0, n - 1);
                    }
                    else            // if n is even
                    {
                        // swap(i, n - 1);
                        T temp = items[i];
                        items[i] = items[n - 1];
                        items[n - 1] = temp;

                    }
                }
            }
        }

    }
}
