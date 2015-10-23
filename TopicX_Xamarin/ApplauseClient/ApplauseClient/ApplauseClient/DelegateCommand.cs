using System;
using System.Windows.Input;

namespace ApplauseClient
{
	public class DelegateCommand : ICommand
	{
		private readonly Action<object> executeAction = delegate { };
		private readonly Predicate<object> canExecute;

		public DelegateCommand(Action<object> execute, Predicate<object> canExe)
		{
			this.executeAction = execute;
			this.canExecute = canExe;
		}

		public bool CanExecute(object parameter)
		{
			if (canExecute == null)
				return true;

			return canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			executeAction(parameter);
		}

		public event EventHandler CanExecuteChanged;
	}
}