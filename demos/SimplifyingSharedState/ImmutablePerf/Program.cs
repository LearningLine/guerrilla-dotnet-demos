using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Caching
{
    class Program
    {
       static void Main(string[] args)
        {
            ResultsCache cache = 
                new SimpleResultsCache();
                //new ImmutableResultsCache();
           
            Console.WriteLine("Using {0} " , cache.GetType().Name);

            for (int nResult = 0; nResult < 8000; nResult++)
            {
                AddFantasyResult(cache,false);
            }
            Console.WriteLine("Press R to create new result, Q to quit");


            CreateCacheReaders(cache, 2);

            bool quit = false;
            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Q:
                        {
                            quit = true;
                        }
                        break;

                    case ConsoleKey.R:
                        {
                            AddFantasyResult(cache,true);
                        }
                        break;
                }
            }
            while (!quit);
        }


        private static void CreateCacheReaders(ResultsCache cache, int nReaders)
        {
            for (int nThreads = 0; nThreads < nReaders; nThreads++)
            {
                Thread t = new Thread(ResultsReader);
                t.IsBackground = true;

                t.Start(cache);
            }
        }

        private static string[] Teams = { "France", "Germany", "Spain", "Portugal", "Italy", "Switzerland", "Poland", "Turkey", "Greece", "Romania", "Croatia" };


        private static Random rnd = new Random();

        private static void AddFantasyResult(ResultsCache cache,bool showResult)
        {
            int firstTeam = 0;
            int secondTeam = 0;
            while (firstTeam == secondTeam)
            {
                firstTeam = rnd.Next(Teams.Length);
                secondTeam = rnd.Next(Teams.Length);
            }

            MatchResult result = new MatchResult { FirstTeam = Teams[firstTeam], SecondTeam = Teams[secondTeam], FirstTeamScore = rnd.Next(5), SecondTeamScore = rnd.Next(5) };

            if (showResult)
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Result just in {0}                                                          ", result.ToString());
            }

            cache.AddResult(result);
        }
       
        private static void ResultsReader(object o)
        {
            Stopwatch timer = Stopwatch.StartNew();
            long nResults = 0;
            ResultsCache cache = (ResultsCache)o;

            while (true)
            {
                for (int i = 0; i < 1000 ; i++)
                {
                    foreach (MatchResult result in cache.GetResults("Germany"))
                    {
                        nResults++;
                    }
                   
                }
                
                Console.SetCursorPosition(0, 4 + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Speed : {0} Results/s      ", (double)nResults / timer.ElapsedMilliseconds);
            }
        }
    }
}
