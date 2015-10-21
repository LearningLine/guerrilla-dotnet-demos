using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    public interface IResultsCache
    {
        IEnumerable<MatchResult> GetResults(string country);
        void AddResult(MatchResult result);

    }
}
