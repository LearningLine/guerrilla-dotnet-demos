using System;
using IssueTracker.Data;

namespace IssueTracker.Logic
{
    public class IssueViewModel : Bindable
    {
        private Issue _issue;

        public IssueViewModel(Issue word)
        {
            _issue = word;
        }

        public Issue Issue { get { return _issue; } }

        public string Text
        {
            get { return _issue.Text; }
        }

        public int Count
        {
            get { return _issue.ReportCount; }
        }

        private DateTime _lastChange;
        public DateTime LastChange
        {
            get { return _lastChange; }
            private set
            {
                if (_lastChange == value)
                    return;
                _lastChange = value;
                OnPropertyChanged();
            }
        }

        public void Update(DateTime time)
        {
            LastChange = time;
            OnPropertyChanged(() => Count);
        }
    }
}
