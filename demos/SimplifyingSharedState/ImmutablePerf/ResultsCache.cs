using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

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
        protected List<MatchResult> results = new List<MatchResult>();

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


    class ImmutableResultsCache : ResultsCache
    {
        private ImmutableList<MatchResult> immutableResults = ImmutableList<MatchResult>.Empty;
        public override IEnumerable<MatchResult> GetResults(string country)
        {
            return from result in immutableResults
                   where result.FirstTeam == country || result.SecondTeam == country
                   select result;
        }

        public override void AddResult(MatchResult result)
        {
            immutableResults = immutableResults.Add(result);
        }
    }


}
