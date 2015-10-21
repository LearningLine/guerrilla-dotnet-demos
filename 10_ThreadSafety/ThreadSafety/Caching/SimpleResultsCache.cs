using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Caching
{
    internal class SimpleResultsCache : IResultsCache
    {
        volatile List<MatchResult> results = new List<MatchResult>();
        public IEnumerable<MatchResult> GetResults(string country)
        {
            return results.Where(r => r.FirstTeam == country || r.SecondTeam == country);
        }

        public void AddResult(MatchResult result)
        {
            results.Add(result);
        }
    }

	internal class ReaderWriterResultsCache : IResultsCache
	{
		volatile List<MatchResult> results = new List<MatchResult>();
		ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
		public IEnumerable<MatchResult> GetResults(string country)
		{
			readerWriterLock.EnterReadLock();
			try
			{
				return results.Where(r => r.FirstTeam == country || r.SecondTeam == country).ToList();
			}
			finally
			{
				readerWriterLock.ExitReadLock();
			}
		}

		public void AddResult(MatchResult result)
		{
			readerWriterLock.EnterWriteLock();

			try
			{
				results.Add(result);
			}
			finally
			{
				readerWriterLock.ExitWriteLock();
			}
		}
	}

	internal class MonitorResultsCache : IResultsCache
	{
		volatile List<MatchResult> results = new List<MatchResult>();
		object lockObj = new object();
		public IEnumerable<MatchResult> GetResults(string country)
		{
			Monitor.Enter(lockObj);
			try
			{
				return results.Where(r => r.FirstTeam == country || r.SecondTeam == country).ToList();
			}
			finally
			{
				Monitor.Exit(lockObj);
			}
		}

		public void AddResult(MatchResult result)
		{
			Monitor.Enter(lockObj);
			try
			{
				results.Add(result);
			}
			finally
			{
				Monitor.Exit(lockObj);
			}
		}
	}

	internal class LatestResultsCache
	{
		ConcurrentDictionary<string,MatchResult> results 
			= new ConcurrentDictionary<string, MatchResult>();

		public MatchResult GetLatestResult(string country)
		{
			MatchResult result;
			
			return results.TryGetValue(country, out result) ? result : null;
		}

		public void AddResult(MatchResult result)
		{
			results.AddOrUpdate(result.FirstTeam,result,(t,m) => result);
		}
	}


	//Only modify if you are a ninja
	internal class OptimalResultsCache : IResultsCache
	{
		volatile List<MatchResult> results = new List<MatchResult>();
		public IEnumerable<MatchResult> GetResults(string country)
		{
			return results
				.Where(r => r.FirstTeam == country || r.SecondTeam == country);

		}

		public void AddResult(MatchResult result)
		{
			var newList = results.ToList();
			newList.Add(result);
			results = newList;
		}
	}

}