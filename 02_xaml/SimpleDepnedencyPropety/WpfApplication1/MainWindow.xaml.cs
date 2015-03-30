using System;
using System.Linq;
using System.Windows;


namespace WpfApplication1
{
	public partial class MainWindow : Window
	{
		public bool IsVisible2
		{
			get { return (bool)GetValue(IsVisibleProperty2); }
			set { SetValue(IsVisibleProperty2, value); }
		}

		// Using a DependencyProperty as the backing store for IsVisible.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsVisibleProperty2 =
			DependencyProperty.Register("IsVisible2", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

		public MainWindow()
		{
			InitializeComponent();
		}
	}
}
