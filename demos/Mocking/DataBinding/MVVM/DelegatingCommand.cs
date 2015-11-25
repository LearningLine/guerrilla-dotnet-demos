using System;
using System.Windows.Input;

namespace MVVM
{
    public class DelegatingCommand : ICommand
    {
        private readonly Action execute;
        private bool canExecute = false;

        public DelegatingCommand(Action execute) : this(execute, true)
        {
        }

        public DelegatingCommand(Action execute, bool canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;

        }

        public bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public void UpdateCanExecute(bool canExecute)
        {
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            execute();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}