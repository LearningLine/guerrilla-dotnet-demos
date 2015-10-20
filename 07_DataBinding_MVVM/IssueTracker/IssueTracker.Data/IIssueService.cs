using System;
using System.Collections.Generic;

namespace IssueTracker.Data
{
    public interface IIssueService
    {
        IEnumerable<Issue> GetAllIssues();
        IEnumerable<Issue> GetActiveIssues();
        Issue GetIssue(string text);
        Issue ReportIssue(string text, User user);
        void ResolveIssue(Issue issue, string fix, DateTime found);
    }
}