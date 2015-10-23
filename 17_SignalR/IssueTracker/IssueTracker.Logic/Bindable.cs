using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Mvvm;

namespace IssueTracker.Logic
{
    public abstract class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler changedEventHandler = PropertyChanged;
            if (changedEventHandler != null)
                changedEventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            OnPropertyChanged(PropertySupport.ExtractPropertyName(propertyExpression));
        }
    }
}
