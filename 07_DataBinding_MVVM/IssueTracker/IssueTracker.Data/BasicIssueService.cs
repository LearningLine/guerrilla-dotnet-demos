using System;
using System.Collections.Generic;
using System.Linq;

namespace IssueTracker.Data
{
    public class BasicIssueService : IIssueService
    {
        private List<Issue> _issues = new List<Issue>();

        public IEnumerable<Issue> GetAllIssues()
        {
            return _issues;
        }

        public IEnumerable<Issue> GetActiveIssues()
        {
            return _issues.Where(i => i.Fixes == null || !i.Fixes.Any());
        }

        public Issue GetIssue(string text)
        {
            return _issues.FirstOrDefault(w => w.Text == text);
        }

        public Issue ReportIssue(string text, User user)
        {
            Issue match = _issues.FirstOrDefault(w => w.Text == text);
            if (match == null)
            {
                match = new Issue { Text = text, Users = new List<User>() };
                _issues.Add(match);
            }

            match.ReportCount++;
            if (!match.Users.Contains(user))
                match.Users.Add(user);

            return match;
        }

        public void ResolveIssue(Issue issue, string fix, DateTime found)
        {
            issue.Fixes.Add(new Resolution
            {
                FixDescription = fix,
                Created = found
            });
        }
    }
}
