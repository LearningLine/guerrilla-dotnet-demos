using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using IssueTracker.Data;
using IssueTracker.MongoData;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Practices.Prism.Commands;

namespace IssueTracker.Logic
{
    public class MainViewModel : Bindable
    {
        private DelegateCommand<string> _loginCommand;
        private User _user = null;
        private ObservableCollection<IssueViewModel> _issues = new ObservableCollection<IssueViewModel>();
        private IIssueService _service;

        public MainViewModel()
        {
            _service = new MongoIssueService();
            //_service = new BasicIssueService();

            _loginCommand = new DelegateCommand<string>(Login, s => CanLogin());

            EnterWordCommand = new DelegateCommand(EnterIssue, () => !String.IsNullOrWhiteSpace(IssueText));
            GetActiveCommand = new DelegateCommand(GetActiveIssues, () => ReportCount > 0);

            UpdateReportCount();

            InitLiveUpdates();
        }

        private async void InitLiveUpdates()
        {
            var connection = new HubConnection("http://localhost:61734");
            IHubProxy hub = connection.CreateHubProxy("tracking");
            hub.On("issueChanged", UpdateFromChangedIssue);
            hub.On("issueFixed", UpdateFromFixedIssue);
            await connection.Start();

            await hub.Invoke("WatchFixes");
            //await hub.Invoke("RegisterForAllFixNotifications");
        }

        private void UpdateFromChangedIssue(dynamic id)
        {
            UpdateReportCount();
            IssueViewModel item = _issues.FirstOrDefault(vm => vm.Issue.Id.ToString() == id);
            if (item != null)
            {
                var saved = _service.ActiveIssues.FirstOrDefault(i => i.Id == item.Issue.Id);
                if (saved != null)
                {
                    item.Issue.ReportCount = saved.ReportCount;
                    item.Issue.Fixes = saved.Fixes;
                    item.Update(DateTime.Now);
                }
            }
        }

        private void UpdateFromFixedIssue(dynamic id)
        {
            UpdateReportCount();
            IssueViewModel item = _issues.FirstOrDefault(vm => vm.Issue.Id.ToString() == id);
            if (item != null)
            {
                _issues.Remove(item);
            }
        }

        public User User
        {
            get { return _user; }
            private set
            {
                if (_user == value)
                    return;
                _user = value;
                OnPropertyChanged();

                OnPropertyChanged(() => IsLoggedOut);
            }
        }

        private string _issueText;
        public string IssueText
        {
            get { return _issueText; }
            set
            {
                if (_issueText == value)
                    return;
                _issueText = value;
                OnPropertyChanged();

                EnterWordCommand.RaiseCanExecuteChanged();
            }
        }
        
        public bool IsLoggedOut
        {
            get { return User == null; }
        }

        public string UserName
        {
            get { return User.Name; }
        }

        public IEnumerable<IssueViewModel> Issues
        {
            get { return _issues; }
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
        }

        public DelegateCommand EnterWordCommand { get; private set; }
        public DelegateCommand GetActiveCommand { get; private set; }

        private IssueViewModel _selectedIssue;
        public IssueViewModel SelectedIssue
        {
            get { return _selectedIssue; }
            set
            {
                if (_selectedIssue == value)
                    return;
                _selectedIssue = value;
                OnPropertyChanged();

                if (_selectedIssue != null)
                {
                    IssueText = _selectedIssue.Text;
                }
            }
        }

        private int _reportCount;
        public int ReportCount
        {
            get { return _reportCount; }
            set
            {
                if (_reportCount == value)
                    return;
                _reportCount = value;
                OnPropertyChanged();
            }
        }
        
        private bool CanLogin()
        {
            return User == null;
        }

        private void Login(string username)
        {
            User = _service.GetUser(username) ?? new User(username);
            _loginCommand.RaiseCanExecuteChanged();
        }

        private void GetActiveIssues()
        {
            foreach (Issue issue in _service.ActiveIssues)
            {
                if (!_issues.Any(ivm => ivm.Issue.LogicalEquals(issue)))
                {
                    _issues.Add(new IssueViewModel(issue));
                }
            }
        }

        private void EnterIssue()
        {
            DateTime reportTime = DateTime.Now;

            IssueViewModel issueVm = _issues.FirstOrDefault(i => i.Text == IssueText);

            if (issueVm == null)
            {
                Issue issue = _service.GetIssue(IssueText) ?? new Issue { Text = IssueText };
                issueVm = new IssueViewModel(issue);
                _issues.Add(issueVm);
            }

            _service.ReportIssue(issueVm.Issue, _user);

            issueVm.Update(reportTime);

            KeepOnlyLatestIssueVms();
            SelectedIssue = null;
            IssueText = "";
            UpdateReportCount();
        }

        private void KeepOnlyLatestIssueVms()
        {
            foreach (IssueViewModel vm in _issues.Except(_issues.OrderByDescending(wvm => wvm.LastChange).Take(5)).ToList())
            {
                _issues.Remove(vm);
            }
        }

        private void UpdateReportCount()
        {
            ReportCount = _service.ActiveIssues.Sum(a => a.ReportCount);
        }
    }
}
