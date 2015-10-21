using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Caching
{
	class OldProgram
    {
        private static volatile bool quit = false;
        static void Main(string[] args)
        {
            //IResultsCache cache = new ReaderWriterResultsCache();
            IResultsCache cache = new OptimalResultsCache();

            AddResultsUntilFirstTeamHasEnough(cache, 1000);

            var readerThreads = CreateCacheReaders(cache, 20);

			//I put this here to go BANG
			resetEventSlim.Set();

            Console.WriteLine("Press W to write a new result, Q to quit");
            do
            {
                while (Console.KeyAvailable == false)
                {
                    Thread.Sleep(100);
                }

                var consoleKey = Console.ReadKey(true).Key;
                if (consoleKey == ConsoleKey.Q)
                {
                    quit = true;
                }
                else if (consoleKey == ConsoleKey.W)
                {
                    var matchResult = MatchResult.CreateRandom();
                    cache.AddResult(matchResult);
                    Console.WriteLine(matchResult);
                }
            }
            while (!quit);

            WaitForReaders(readerThreads);
        }

        private static void WaitForReaders(List<Thread> threads)
        {
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

		static ManualResetEventSlim resetEventSlim = new ManualResetEventSlim();

        private static List<Thread> CreateCacheReaders(IResultsCache cache, int numReaders)
        {
            var list = new List<Thread>();
            for (int nThreads = 0; nThreads < numReaders; nThreads++)
            {
                Thread t = new Thread(ResultsReader) { IsBackground = true };
                list.Add(t);
                t.Start(cache);
            }
            return list;
        }

        private static void ResultsReader(object o)
        {
	        Console.WriteLine("Starting");
	        resetEventSlim.Wait();

	        Console.WriteLine("Started");
            long nResults = 0;
            IResultsCache cache = (IResultsCache)o;
            var firstCountry = MatchResult.Countries.First();

            Stopwatch timer = Stopwatch.StartNew();
            while (!quit)
            {
                nResults += cache.GetResults(firstCountry).LongCount();
            }
            Console.WriteLine("Speed: {0} results/ms", (double)nResults / timer.ElapsedMilliseconds);
        }

        private static void AddResultsUntilFirstTeamHasEnough(IResultsCache cache, int numberOfScores)
        {
            var firstCountry = MatchResult.Countries.First();
            while (cache.GetResults(firstCountry).Count() < numberOfScores)
            {
                cache.AddResult(MatchResult.CreateRandom());
            }
        }
    }
}
