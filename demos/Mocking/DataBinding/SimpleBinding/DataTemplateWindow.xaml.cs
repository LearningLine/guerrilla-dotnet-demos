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
using System.Windows.Shapes;

namespace SimpleBinding
{
    /// <summary>
    /// Interaction logic for DataTemplateWindow.xaml
    /// </summary>
    public partial class DataTemplateWindow : Window
    {
        public DataTemplateWindow()
        {
            InitializeComponent();

            Content = new Person {Name="Andy", Age=0x37};
        }
    }
}
