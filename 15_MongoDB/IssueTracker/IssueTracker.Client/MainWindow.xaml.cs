using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IssueTracker.Logic;

namespace IssueTracker.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainViewModel viewModel = DataContext as MainViewModel;
            if (viewModel != null)
            {
                var user = Properties.Settings.Default.User;
                if (!String.IsNullOrWhiteSpace(user))
                {
                    UserName.Text = user;
                    viewModel.LoginCommand.Execute(user);
                }
                else
                {
                    viewModel.PropertyChanged += (sender, e) =>
                    {
                        if (e.PropertyName == "User")
                        {
                            Properties.Settings.Default.User = viewModel.UserName;
                        }
                    };
                }
            }
        }
    }
}
