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

namespace ConfigureAwaitGotcha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string punchline = "To get to the other side";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ResulTextBlock.Text = CreatePunchLine();
        }

        private string CreatePunchLine()
        {
            return String.Join( " " ,Shuffle(punchline.Split(' ')));
        }

        private string[] Shuffle(string[] split)
        {
       

            Random rnd = new Random();
            for (int i = 0; i < split.Length; i++)
            {
                string temp = split[i];
                int j = rnd.Next(split.Length);
                split[i] = split[j];
                split[j] = temp;
            }

            return split;
        }
    }
}
