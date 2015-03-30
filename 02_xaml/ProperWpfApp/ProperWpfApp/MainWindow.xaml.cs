using System.Windows;

namespace ProperWpfApp
{
	public class Employee
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Position { get; set; }

	}


	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
			this.DialogResult = true;
		}

		public Employee GetEmployee()
		{
			var employee = new Employee();
			employee.FirstName = this.TextBoxFirstName.Text;
			employee.LastName = this.TextBoxLastName.Text;

			return employee;
		}
	}
}
