using System.Collections.Generic;

namespace IssueTracker.Data
{
    public static class DataExtensions
    {
        public static bool LogicalEquals(this Issue issue, Issue other)
        {
            if (other == null)
                return false;
            return other.Id == issue.Id;
        }
    }
}
