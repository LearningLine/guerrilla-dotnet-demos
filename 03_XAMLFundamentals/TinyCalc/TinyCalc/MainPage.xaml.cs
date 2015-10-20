using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TinyCalc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int _currentValue;
        private bool _nextKeypressClears;



        public int CurrentValue
        {
            get { return (int)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(int),
                typeof(MainPage), new PropertyMetadata(0));



        public MainPage()
        {
            this.InitializeComponent();
        }

        private void buttonZero_Click(object sender, RoutedEventArgs e)
        {
            if (_nextKeypressClears)
                textBox.Text = "";
            textBox.Text += "0";
        }

        private void buttonOne_Click(object sender, RoutedEventArgs e)
        {
            if (_nextKeypressClears)
                textBox.Text = "";
            textBox.Text += "1";
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            int val;
            if (int.TryParse(textBox.Text, out val))
            {
                CurrentValue += val;
                textBox.Text = CurrentValue.ToString();
                _nextKeypressClears = true;
            }
        }
    }
}
