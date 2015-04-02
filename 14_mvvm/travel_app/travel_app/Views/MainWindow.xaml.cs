using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using travel_app.ViewModels;
using TravelApp.Data;

namespace travel_app
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			DataContext = new MainWindowViewModel();
			InitializeComponent();
		}
	}
}
