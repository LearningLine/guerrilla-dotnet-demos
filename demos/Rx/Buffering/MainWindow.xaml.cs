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
using System.Threading.Tasks;
using System.Threading;



namespace Buffering
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Combiner combi = new Combiner();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = combi;
        }

        IEnumerable<int> GenerateData()
        {
            while (true)
            {
                yield return 1;
                Thread.Sleep(0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SynchronizationContext ctx = SynchronizationContext.Current;
            Task t = Task.Factory.StartNew(() =>
                {
                    //foreach (var item in GenerateData())
                    //{
                    //    ctx.Post(o => combi.Value++, null);
                    //}

                    GenerateData()
                        .ToObservable()
                        .Buffer(TimeSpan.FromMilliseconds(100))
                        .Subscribe(buffer =>
                        {
                            ctx.Post(o => combi.Value+= buffer.Count, null);
                        });

                });

        }
    }
}
















