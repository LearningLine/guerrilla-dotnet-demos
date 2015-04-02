using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelApp.Data;

namespace travel_app
{
	public partial class MainWindow : Window
	{
		private Destination[] destinations = Db.AllDestinations();
		private ObservableCollection<Destination> data = new ObservableCollection<Destination>();

		public MainWindow()
		{
			InitializeComponent();
			destinations.Take(5).ToList().ForEach(d => data.Add(d));
			//data.AddRange(destinations.Take(5).ToArray());
			DataContext = data;
		}

		private void ShowDetails(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ListBox listBox = (ListBox) sender;

			Destination d = listBox.SelectedItem as Destination;
			if (d == null)
			{
				return;
			}

			var win = new TravelDetails(d);
			win.ShowDialog();

		}

		private void onKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.A)
			{
				var d = destinations[5];
				data.Add(d);
				MessageBox.Show("Added new location");
			}
		}

	}
}
