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

namespace WPFandXAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZoomOut(object sender, RoutedEventArgs e)
        {
            zoomer.ScaleX /= 1.25;
            zoomer.ScaleY /= 1.25;
        }

        private void ZoomIn(object sender, RoutedEventArgs e)
        {
            zoomer.ScaleX *= 1.25;
            zoomer.ScaleY *= 1.25;
        }
    }
}
