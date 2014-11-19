using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ThreadedUi
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double val = 0;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TheButton.IsEnabled = false;
           // var ctx = SynchronizationContext.Current;

            var t = Task.Factory.StartNew(() =>
            {
                var r = CalcPi();
                return r;
            });
            t.ContinueWith(tPrev => TheButton.IsEnabled = true, TaskScheduler.FromCurrentSynchronizationContext());
            t.ContinueWith(tPrev =>
            {
               // ctx.Post(_ => { ResulTextBlock.Text = tPrev.Result.ToString(); }, null);
                ResulTextBlock.Text = tPrev.Result.ToString();
            }, TaskScheduler.FromCurrentSynchronizationContext());


        }

        private double CalcPi()
        {
            Thread.Sleep(4000);
            val += 1;
           
            return val;
        }
    }
}