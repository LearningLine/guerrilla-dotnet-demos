using System;
using System.Collections.Generic;

namespace IssueTracker.Data
{
    public interface IIssueService
    {
        IEnumerable<Issue> AllIssues { get; }
        Issue GetIssue(string text);
        void ReportIssue(Issue issue, User user);
    }
}