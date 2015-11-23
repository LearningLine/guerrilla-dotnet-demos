using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Media;

namespace AnonymousMethodAndCapture
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            sounds.ItemsSource = new DirectoryInfo(@"C:\windows\media")
                .GetFiles("*.wav")
                .Select( f => new SoundFile { FullPath = f.FullName, SimpleName = f.Name });

        }

        private void AddSoundButton(object sender, RoutedEventArgs e)
        {
            Button button = new Button() { Content = message.Text };

            SoundFile sound = (SoundFile)(sounds.SelectedValue);
            string msg = message.Text;

           
            buttons.Children.Add(button);

        }
    }

    public class SoundFile
    {
        public string SimpleName { get; set; }
        public string FullPath { get; set; }
    }
}
