using System;
using System.Collections.Generic;
using System.Linq;

namespace IssueTracker.Data
{
    public class BasicIssueService : IIssueService
    {
        private List<Issue> _issues = new List<Issue>();

        public IEnumerable<Issue> AllIssues
        {
            get { return _issues; }
        }

        public Issue GetIssue(string text)
        {
            return _issues.FirstOrDefault(w => w.Text == text);
        }

        public void ReportIssue(Issue issue, User user)
        {
            if (!_issues.Contains(issue))
                _issues.Add(issue);

            issue.ReportCount++;
            if (!issue.Users.Contains(user))
                issue.Users.Add(user);
        }
    }
}
