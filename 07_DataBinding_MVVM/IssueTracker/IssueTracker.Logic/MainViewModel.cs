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
    public class MainViewModel : Bindable
    {
        private ObservableCollection<IssueViewModel> _issues = new ObservableCollection<IssueViewModel>();
        private IIssueService _service;
        private bool isLoggedIn;

        public string IssueText { get; set; }

        public DelegateCommand<string> LoginCommand { get; set; }
        public DelegateCommand EnterIssueCommand { get; set; }

        public MainViewModel()
        {
            _service = new BasicIssueService();
            LoginCommand = new DelegateCommand<string>(Login, CanLogin);
            EnterIssueCommand = new DelegateCommand(EnterIssue);
        }

        public User User { get; private set; }

        public IEnumerable<IssueViewModel> Issues
        {
            get { return _issues; }
        }

        public bool IsLoggedIn
        {
            get { return User!= null; }
        }

        private bool CanLogin(string userName)
        {
            return User == null;
        }

        private void Login(string username)
        {
            User = new User { Name = username };
            LoginCommand.RaiseCanExecuteChanged();
            OnPropertyChanged("IsLoggedIn");
        }

        private void EnterIssue()
        {
            DateTime reportTime = DateTime.Now;
            Issue issue = _service.GetIssue(IssueText);
            if (issue == null)
            {
                issue = new Issue { Text = IssueText, Users = new List<User>() };
            }
            _service.ReportIssue(issue, User);

            var issueViewModel = _issues.FirstOrDefault(ivm => ivm.Issue == issue);
            if (issueViewModel == null)
            {
                issueViewModel = new IssueViewModel(issue);
                _issues.Add(issueViewModel);
            }

            issueViewModel.Update(reportTime);
        }
    }
}