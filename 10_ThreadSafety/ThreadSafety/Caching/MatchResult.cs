using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return String.Format("{0} {1} - {2} {3}", FirstTeam, FirstTeamScore, SecondTeam, SecondTeamScore);
        }

        public static string[] Countries = { "USA", "Germany", "France", "Japan", "England", "Korea", "Brazil", "Sweden", "Australia", "Norway", "Canada", "Netherlands", "Italy", "Denmark", };
        private static Random rnd = new Random();

        public static MatchResult CreateRandom()
        {
            int firstTeam = 0; int secondTeam = 0;
            while (firstTeam == secondTeam)
            {
                firstTeam = rnd.Next(Countries.Length);
                secondTeam = rnd.Next(Countries.Length);
            }
            return new MatchResult
            {
                FirstTeam = Countries[firstTeam],
                SecondTeam = Countries[secondTeam],
                FirstTeamScore = rnd.Next(5),
                SecondTeamScore = rnd.Next(5)
            };
        }

    }
}
