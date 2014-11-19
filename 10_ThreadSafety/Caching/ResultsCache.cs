using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Caching
{

    public class MatchResult
    {
        public string FirstTeam { get; set; }
        public int FirstTeamScore { get; set; }

        public string SecondTeam { get; set; }
        public int SecondTeamScore { get; set; }

        public override string ToString()
        {
            return String.Format("{0} : {1} {2}:{3}", FirstTeam, FirstTeamScore, SecondTeam, SecondTeamScore);
        }
    }

    public abstract class ResultsCache
    {
        protected  volatile List<MatchResult> results = new List<MatchResult>();

        public abstract IEnumerable<MatchResult> GetResults(string country);
        public abstract void AddResult(MatchResult resultsToAdd);

    }

    class SimpleResultsCache : ResultsCache
    {

        public override IEnumerable<MatchResult> GetResults(string country)
        {
            return from result in results
                   where result.FirstTeam == country || result.SecondTeam == country
                   select result;
        }

        public override void AddResult(MatchResult result)
        {
            results.Add(result);
        }
    }

    class MonitorResultsCache : ResultsCache
    {
        private object resultsLock = new object();

        public override IEnumerable<MatchResult> GetResults(string country)
        {
            lock (resultsLock)
            {
                return (from result in results
                    where result.FirstTeam == country || result.SecondTeam == country
                    select result).ToList();
            }
        }

        public override void AddResult(MatchResult result)
        {
            lock (resultsLock)
            {
                results.Add(result);
            }
        }
    }

    class RWLockResultsCache : ResultsCache
    {
        private ReaderWriterLockSlim resultsLock = new ReaderWriterLockSlim();

        public override IEnumerable<MatchResult> GetResults(string country)
        {
            resultsLock.EnterReadLock();
            try
            {
                return (from result in results
                    where result.FirstTeam == country || result.SecondTeam == country
                    select result).ToList();
            }
            finally
            {
                resultsLock.ExitReadLock();
            }
        }

        public override void AddResult(MatchResult result)
        {
          resultsLock.EnterWriteLock();
            try
            {
                results.Add(result);
            }
            finally
            {
                resultsLock.ExitWriteLock();
            }
        }
    }

    class OptimalResultsCache : ResultsCache
    {

        public override IEnumerable<MatchResult> GetResults(string country)
        {
            return from result in results
                   where result.FirstTeam == country || result.SecondTeam == country
                   select result;
        }

        // This assumes that there is only one writer
        // if there is this needs to be made atomic
        //
        public override void AddResult(MatchResult result)
        {
            var newResults = new List<MatchResult>(results);

            newResults.Add(result);
            // Refernce assignment is atomic
            results = newResults;
        }
    }

    class BagResultsCache : ResultsCache
    {
        ConcurrentBag<MatchResult>  bagOfResults = new ConcurrentBag<MatchResult>();
        public override IEnumerable<MatchResult> GetResults(string country)
        {
            return from result in bagOfResults
                   where result.FirstTeam == country || result.SecondTeam == country
                   select result;
        }

        // This assumes that there is only one writer
        // if there is this needs to be made atomic
        //
        public override void AddResult(MatchResult result)
        {
          bagOfResults.Add(result);
        }
    }


}














