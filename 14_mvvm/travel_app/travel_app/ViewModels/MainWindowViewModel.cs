using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelApp.Data;

namespace travel_app.ViewModels
{
	static class ExeObservable
	{
		public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> stuff)
		{
			ObservableCollection<T> coll = new ObservableCollection<T>();
			foreach (var v in stuff)
			{
				coll.Add(v);
			}
			return coll;
		} 
	}
	class MainWindowViewModel : BaseModel
	{
		public ObservableCollection<Destination> Destinations { get; set; }

		public ICommand ShowDetails { get; set; }

		public MainWindowViewModel()
		{
			this.ShowDetails = new DelegatingCommand(OnShowDetails);
			Destinations = Db.AllDestinations().ToObservable();
		}

		public Destination SelectedDestination { get; set; }

		private void OnShowDetails(object obj)
		{
			if (SelectedDestination == null)
			{
				return;
			}

			var win = new TravelDetails(SelectedDestination);
			win.ShowDialog();

		}
	}
}
