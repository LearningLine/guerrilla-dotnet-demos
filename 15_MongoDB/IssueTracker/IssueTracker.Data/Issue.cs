using System.Collections.Generic;
using MongoDB.Bson;

namespace IssueTracker.Data
{
    public class Issue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ReportCount { get; set; }
        public List<Resolution> Fixes { get; set; }
    }
}