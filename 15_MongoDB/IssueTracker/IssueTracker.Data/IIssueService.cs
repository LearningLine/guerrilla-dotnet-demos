using System;
using System.Collections.Generic;

namespace IssueTracker.Data
{
    public interface IIssueService
    {
        User GetUser(string name);
        IEnumerable<Issue> AllIssues { get; }
        IEnumerable<Issue> ActiveIssues { get; }
        Issue GetIssue(string text);
        void ReportIssue(Issue issue, User user);
        void ReportExistingIssue(Issue issue);
        void ResolveIssue(Issue issue, string fix, DateTime found);
    }
}