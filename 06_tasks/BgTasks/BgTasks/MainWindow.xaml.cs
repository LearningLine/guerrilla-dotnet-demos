using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BgTasks
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private TaskScheduler uiScheduler;

		public MainWindow()
		{
			InitializeComponent();
			uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(StartWork);
			//StartWork();
		}

		private void StartWork()
		{
			for (int i = 0; i < 100; i++)
			{
				// Computational
				Thread.Sleep(100);

				// UI
				Task updateUI = new Task(o => progressBar.Value = (int)o, i);
				updateUI.Start(uiScheduler);
			}
		}
	}
}
