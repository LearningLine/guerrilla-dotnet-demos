using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTracker.Data;
using Microsoft.Practices.Prism.Commands;

namespace IssueTracker.Logic
{
    public class MainViewModel
    {
        private ObservableCollection<IssueViewModel> _issues = new ObservableCollection<IssueViewModel>();
        private IIssueService _service;

        public MainViewModel()
        {
            _service = null;
        }

        public User User { get; private set; }


        private bool CanLogin()
        {
            return User == null;
        }

        private void Login(string username)
        {
            User = new User { Name = username };
        }

        private void EnterIssue()
        {
            string issueText = "";

            DateTime reportTime = DateTime.Now;
            Issue issue = _service.GetIssue(issueText);
            if (issue == null)
            {
                issue = new Issue { Text = issueText, Users = new List<User>() };
            }
            _service.ReportIssue(issue, User);


        }
    }
}