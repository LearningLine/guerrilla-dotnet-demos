using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geographical;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace MontiCarlo
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(@"..\..\..\uk-Postcodes-Towns.csv");

            TripSerialiser serialiser = new TripSerialiser(map);

            Trip trip =  serialiser.Load(@"..\..\15initial.xml");
            Trip actualShortest = serialiser.Load(@"..\..\15shortest.xml");

            Trip route = trip;

            AverageBenchmark(trip,actualShortest);
        }

        private static void AverageBenchmark(Trip trip, Trip actualShortest)
        {
            double average = 0;
            int nRuns = 10;

            for (int nRun = 0; nRun < nRuns; nRun++)
            {
                Console.Write("{0}", nRun);
                int nCalcs = Environment.ProcessorCount;

                Task<Trip>[] candidates = new Task<Trip>[nCalcs];

                Trip route = trip;
                for (int nTask = 0; nTask < nCalcs; nTask++)
                {
                    candidates[nTask] =
                        Task.Factory.StartNew<Trip>(() => FindShortestRoute(route, new TimeSpan(0, 0, 2)));
                }

                for (int nTask = 0; nTask < nCalcs; nTask++)
                {
                    if (candidates[nTask].Result.TotalDistance < route.TotalDistance)
                    {
                        route = candidates[nTask].Result;
                    }
                }

                double percentageOut = (route.TotalDistance - actualShortest.TotalDistance)/
                                       (double) actualShortest.TotalDistance*
                                       100;
                Console.WriteLine(",{0}",percentageOut);


                average += percentageOut;

            }
            Console.WriteLine();
            Console.WriteLine("Average {0}" , average / nRuns );
        }

        private static void RandomiseArray<T>(T[] items)
        {
            Random rnd = new Random();

            for (int nTime = 0; nTime < 1000000; nTime++)
            {
                for (int nItem = 0; nItem < items.Length; nItem++)
                {
                    int lhs = rnd.Next(items.Length);
                    int rhs = rnd.Next(items.Length);

                    T tmp = items[lhs];
                    items[lhs] = items[rhs];
                    items[rhs] = tmp;
                }
            }

            
        }

        private static void PrintTrip(Trip route)
        {
            Console.WriteLine(route.Start);
            foreach (MapLocation wayPoint in route.WayPoints)
            {
                Console.WriteLine(wayPoint);
            }
            Console.WriteLine(route.End);
        }

        private static Trip FindShortestRoute(Trip trip, TimeSpan timeSpan)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Trip shortestTrip = trip;

            Stopwatch timer = Stopwatch.StartNew();
            do
            {
                MapLocation[] placesToVisit = shortestTrip.WayPoints.ToArray();

                int lhs = rnd.Next(placesToVisit.Length);
                int rhs = rnd.Next(placesToVisit.Length);

                MapLocation temp = placesToVisit[lhs];
                placesToVisit[lhs] = placesToVisit[rhs];
                placesToVisit[rhs] = temp;

                Trip candidateTrip = new Trip(trip.Start, trip.End, placesToVisit);

                if (candidateTrip.TotalDistance < shortestTrip.TotalDistance)
                {
                   
                    shortestTrip = candidateTrip;
                }
            }
            while (timer.Elapsed < timeSpan);

            return shortestTrip;
        }
    }
}
