using System.Collections.Generic;

namespace IssueTracker.Data
{
    public class Issue
    {
        public string Text { get; set; }
        public int ReportCount { get; set; }

        public List<User> Users { get; set; }

        public List<Resolution> Fixes { get; set; }
    }
}
