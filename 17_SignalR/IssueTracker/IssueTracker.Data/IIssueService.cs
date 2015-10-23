using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace IssueTracker.Data
{
    public interface IIssueService
    {
        User GetUser(string name);
        IEnumerable<Issue> AllIssues { get; }
        IEnumerable<Issue> ActiveIssues { get; }
        Issue GetIssue(string text);
        void ReportIssue(Issue issue, User user);
        void ReportExistingIssue(string issueId);
        void ResolveIssue(string issueId, string fix, DateTime found);
    }
}