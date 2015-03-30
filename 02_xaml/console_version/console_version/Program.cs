using System;
using System.Windows;
using System.Windows.Controls;

namespace console_version
{
	class Program
	{
		private static Window mainWindow;
		[STAThread]
		static void Main(string[] args)
		{
			Window window = new Window();
			mainWindow = window;
			window.Title = "This is my title!";

			var btn = new Button();
			btn.Content = "CLick me!";

			var view = new Viewbox();
			view.Child = btn;

			btn.Click += btn_Click;

			window.Content = view;

			window.ShowDialog();
		}

		static void btn_Click(object sender, RoutedEventArgs e)
		{
			mainWindow.Title = "Cool, you clicked me!";
		}
	}
}
