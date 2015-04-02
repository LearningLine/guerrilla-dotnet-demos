using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace travel_app.ViewModels
{
	public class DelegatingCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;
		
		private readonly Action<object> executedDelegate; 
		private readonly Func<object, bool> canExecutedDelegate;

		public DelegatingCommand(
			Action<object> executedDelegate, 
			Func<object, bool> canExecutedDelegate = null)
		{
			if (executedDelegate == null) throw new ArgumentNullException("executedDelegate");
		
			this.executedDelegate = executedDelegate;
			this.canExecutedDelegate =
				canExecutedDelegate ?? delegate { return true; };
		}

		public bool CanExecute(object parameter)
		{
			return this.canExecutedDelegate(parameter);
		}

		public void Execute(object parameter)
		{
			this.executedDelegate(parameter);
		}
	}
}
