using System;
using System.Collections.Generic;
using System.Linq;

namespace IssueTracker.Data
{
    public class BasicIssueService : IIssueService
    {
        private List<Issue> _issues = new List<Issue>();

        public User GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Issue> AllIssues
        {
            get { return _issues; }
        }

        public IEnumerable<Issue> ActiveIssues
        {
            get { return _issues.Where(i => i.Fixes == null || !i.Fixes.Any()); }
        }

        public Issue GetIssue(string text)
        {
            return _issues.FirstOrDefault(w => w.Text == text);
        }

        public void ReportIssue(Issue issue, User user)
        {
            throw new NotImplementedException();
            //if (!_issues.Contains(issue))
            //{
            //    issue.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            //    _issues.Add(issue);
            //}

            //issue.ReportCount++;

            //if (!user.ReportedIssues.Contains(issue.Id))
            //    user.ReportedIssues.Add(issue.Id);
        }

        public void ReportExistingIssue(Issue issue)
        {
            issue.ReportCount++;
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
