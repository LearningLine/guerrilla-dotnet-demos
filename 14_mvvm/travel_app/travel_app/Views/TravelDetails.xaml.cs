using System.Windows;
using System.Windows.Controls;
using TravelApp.Data;

namespace travel_app
{
	public partial class TravelDetails : Window
	{
		public Destination Destination { get; set; }

		public TravelDetails(Destination destination)
		{
			Destination = destination;
			InitializeComponent();
			this.DataContext = destination;
		}

		private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			ComboBox combo = (ComboBox) sender;
			this.Destination.Rating = combo.SelectedIndex + 1;
		}
	}
}
