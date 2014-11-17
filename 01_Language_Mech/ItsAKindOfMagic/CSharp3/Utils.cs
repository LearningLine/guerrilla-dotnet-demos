using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSharp3.UtilStuff
{
    static class Utils
    {
        public static string Capitalise(this string source)
        {
            return source.Substring(0, 1).ToUpper() + source.Substring(1);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> test )
        {
            Console.WriteLine("MINE!!");
            foreach (T item in source)
            {
                if (test(item)) yield return item;
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source,
                                                                    Func<TSource, TResult> projection)
        {
            foreach (TSource item in source)
            {
                yield return projection(item);
            }
        }

        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            return new List<T>(source);
        }


    }
}