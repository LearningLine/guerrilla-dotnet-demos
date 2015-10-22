using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace IssueTracker.Data
{
    public class User
    {
        public string Name { get; set; }
        public List<Issue> ReportedIssues { get; set; }

        public User(string name)
        {
            Name = name;
            ReportedIssues = new List<Issue>();
        }

        public User()
        {
        }
    }
}
