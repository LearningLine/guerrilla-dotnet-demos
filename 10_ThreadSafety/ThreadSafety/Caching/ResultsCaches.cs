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
}