using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

namespace UglyInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Observable.FromEventPattern<RoutedEventArgs>(
                this.TheBigDodgerBlueButton,
                "Click")
                .Window(5, 1)
                .Subscribe(wnd =>
                {
                    wnd
                        .Buffer(5)
                        .TimeInterval()
                        .Subscribe(ti =>
                        {
                            if (ti.Interval < TimeSpan.FromSeconds(3))
                            {
                                TheBigDodgerBlueButton.Content = ":-)";
                            }
                        });
                });


            //Observable.FromEventPattern<RoutedEventArgs>(
            //    this.TheBigDodgerBlueButton,
            //    "Click")
            //    .Buffer(5)
            //    .TimeInterval()
            //    .Subscribe(ti =>
            //    {
            //        if (ti.Interval < TimeSpan.FromSeconds(3))
            //        {
            //            TheBigDodgerBlueButton.Content = ":-)";
            //        }
            //        else
            //        {
            //            TheBigDodgerBlueButton.Content = ":-(";
            //        }
            //    });
        }
    }
}
