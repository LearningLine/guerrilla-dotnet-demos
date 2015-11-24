using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace UglyApp
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //SynchronizationContext uiCtx =
            //    SynchronizationContext.Current;

            //Task.Run(() =>
            //{
            //    double pi = 0;
            //    pi = CalculatePi();
            //    uiCtx.Post(_ => ResultTextBlock.Text = pi.ToString(),
            //        null);
            //});

            //Task<double> calcPiTask = Task.Run<double>(() => CalculatePi());

            //calcPiTask.ContinueWith(piTask =>
            //{
               
            //    ResultTextBlock.Text = piTask.Result.ToString();
            //},TaskScheduler.FromCurrentSynchronizationContext());

            Task.Run(() => CalculatePi())
                .ContinueWith(piTask => ResultTextBlock.Text = piTask.Result.ToString(),
                    TaskScheduler.FromCurrentSynchronizationContext());

        }

        public static double CalculatePi()
        {
            Thread.Sleep(3000);
            return Math.PI;
        }
    }
}
