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

namespace WpfEventCapture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MouseEventArgs> values = new List<MouseEventArgs>();

        public MainWindow()
        {
            InitializeComponent();
            this.MouseMove += MainWindow_MouseMove;

            this.MouseDown += PrintValues;
        }

        private void PrintValues(object sender, MouseButtonEventArgs e)
        {
            Content = String.Join(",", values
                .Select(ev => ev.GetPosition(this).Y.ToString()));

        }

        void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            values.Add(e);
        }
    }
}
