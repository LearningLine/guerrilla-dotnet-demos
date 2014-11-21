using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

namespace TheBigRedButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Observable
                .FromEventPattern<RoutedEventArgs>(bigRedSexyButton, "Click")
                .Window(5, 1)
                .Subscribe(w =>
                {
                    w.Buffer(5).TimeInterval()
                        .Where(ti => ti.Interval < TimeSpan.FromSeconds(2))
                        .Subscribe(ti =>
                        {
                            bigRedSexyButton.Content = "STICKY CLICKS!!!";
                        });
                });


            //Observable
            //.FromEventPattern<RoutedEventArgs>(bigRedSexyButton, "Click")
            //.Buffer(5)
            //.TimeInterval()
            //.Where(ti => ti.Interval < TimeSpan.FromSeconds(2))
            //.Subscribe(ti =>
            //{
            //    bigRedSexyButton.Content = "STICKY CLICKS!!!";
            //});

        }
    }
}





