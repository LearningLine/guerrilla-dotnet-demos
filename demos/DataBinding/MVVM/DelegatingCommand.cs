using System;
using System.Windows.Input;

namespace MVVM
{
    public class DelegatingCommand : ICommand
    {
        private readonly Action execute;

        public DelegatingCommand(Action execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}