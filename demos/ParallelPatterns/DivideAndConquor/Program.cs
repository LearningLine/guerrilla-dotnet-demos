using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ParallelSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> items = new List<int>(); 

            Random rnd = new Random();

            for (int i = 0; i < 1000000; i++)
            {
                items.Add(rnd.Next(1000000));

            }


            Console.WriteLine("Sorting..");

            ExerciseSorter(items, ParallelQuickSort);
            ExerciseSorter(items, QuickSort);
           
        }

        private static void ExerciseSorter<T>(List<T> items , Func<List<T>,List<T>> sorter) where T:IComparable<T>
        {
            Stopwatch timer = Stopwatch.StartNew();

            List<T> sorted = sorter(items);

            Console.WriteLine("Sorted {0} took {1}", sorter.Method.Name, timer.Elapsed);

            T prev = sorted[0];
            foreach (T item in sorted)
            {
                if (prev.CompareTo(item) > 0 )
                {
                    Console.WriteLine("Failed!!");
                    return;
                }

                prev = item;
            }
        }

        private static  Random rnd = new Random();

        public static List<T> QuickSort<T>( List<T> items ) where T:IComparable<T>
        {
            if (items.Count <= 1) return items;

            int randomPositon = rnd.Next(items.Count);
            T pivotValue = items[randomPositon];

            List<T> result = QuickSort<T>(items.Where(i => i.CompareTo(pivotValue) < 0).ToList());
            result.AddRange(items.Where(i => i.Equals(pivotValue)));
            result.AddRange( QuickSort<T>( items.Where( i => i.CompareTo(pivotValue) > 0 ).ToList()) );


            return result;
        }

        public static List<T> ParallelQuickSort<T>(List<T> items) where T : IComparable<T>
        {
            if (items.Count <= 1) return items;

            int randomPositon = rnd.Next(items.Count);
            T pivotValue = items[randomPositon];

            Func<List<T>, List<T>> sortFunction = items.Count > 5000 ? (Func < List<T>, List< T >> )ParallelQuickSort : QuickSort;

            Task<List<T>> lhsTask = Task.Run(() => sortFunction(items.Where(i => i.CompareTo(pivotValue) < 0).ToList()));
            Task<List<T>> middleTask = Task.Run(() => items.Where(i => i.Equals(pivotValue)).ToList());
            Task<List<T>> rhsTask = Task.Run(() => sortFunction(items.Where(i => i.CompareTo(pivotValue) > 0).ToList()));

            return lhsTask.Result
                          .Concat(middleTask.Result)
                          .Concat(rhsTask.Result).ToList();
        }



        #region hidden
        //public static List<T> ParallelQuickSort<T>(List<T> items, int depth) where T : IComparable<T>
        //{
        //    if (items.Count <= 1) return items;

        //    int randomPositon = rnd.Next(items.Count);
        //    T pivotValue = items[randomPositon];

        //    Func<List<T>, int, List<T>> sortMethod = ParallelQuickSort;
        //    if (depth > 4) sortMethod = QuickSort;

        //    Task<List<T>> firstSet = Task.Run<List<T>>(() => sortMethod(items.Where(i => i.CompareTo(pivotValue) < 0).ToList(), depth + 1));
        //    Task<List<T>> secondSet = Task.Run(() => items.Where(i => i.Equals(pivotValue)).ToList());
        //    Task<List<T>> thirdSet = Task.Run(() => sortMethod(items.Where(i => i.CompareTo(pivotValue) > 0).ToList(), depth + 1));

        //    var result =
        //        firstSet.Result
        //            .Concat(secondSet.Result)
        //            .Concat(thirdSet.Result).ToList();

        //    return result;
        //}
        #endregion

    }
}
