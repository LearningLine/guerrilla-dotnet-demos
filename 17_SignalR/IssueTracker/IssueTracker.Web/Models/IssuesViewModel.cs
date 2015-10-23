using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueTracker.Data;
using IssueTracker.MongoData;
namespace IssueTracker.Web.Models
{
    public class IssuesViewModel
    {
        private IEnumerable<Issue> _activeIssues;
        private MongoIssueService _service = new MongoIssueService();

        public IssuesViewModel()
        {
            _activeIssues = _service.ActiveIssues;
        }

        public IEnumerable<Issue> ActiveIssues
        {
            get { return _activeIssues; }
        }


    }
}