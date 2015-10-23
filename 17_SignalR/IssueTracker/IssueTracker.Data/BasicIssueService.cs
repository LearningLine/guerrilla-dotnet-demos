using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace IssueTracker.Data
{
    public class BasicIssueService : IIssueService
    {
        private List<Issue> _issues = new List<Issue>();

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
            if (!_issues.Contains(issue))
            {
                issue.Id = MongoDB.Bson.ObjectId.GenerateNewId();
                _issues.Add(issue);
            }

            issue.ReportCount++;

            if (!user.ReportedIssues.Contains(issue.Id))
                user.ReportedIssues.Add(issue.Id);
        }

        public void ResolveIssue(string issueId, string fix, DateTime found)
        {
            var issue = _issues.FirstOrDefault(i => i.Id.ToString() == issueId);
            if (issue != null)
            {
                issue.Fixes.Add(new Resolution
                {
                    FixDescription = fix,
                    Created = found
                });
            }
        }

        public User GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public void ReportExistingIssue(string issueId)
        {
            throw new NotImplementedException();
        }
    }
}
