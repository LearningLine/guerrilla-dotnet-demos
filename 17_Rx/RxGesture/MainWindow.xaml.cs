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

namespace RxGesture
{
    public class Traveller
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();

            var mouseDowns = Observable
                .FromEventPattern<MouseEventArgs>(this, "MouseDown");

            var mouseUps = Observable
                .FromEventPattern<MouseEventArgs>(this, "MouseUp");

            List<Point> gesturePoints = new List<Point>();

            mouseDowns.Subscribe(ep =>
                {
                    gesturePoints.Add(ep.EventArgs.GetPosition(this));

                    Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
                       .TakeUntil(mouseUps)
                       .Subscribe(move =>
                       {
                           gesturePoints.Add(move.EventArgs.GetPosition(this));
                       });
                });

            mouseUps
                .Subscribe(mups =>
                {
                    bool isScrollUp = 
                        gesturePoints.AdjacentMatch((p,n) => p.Y > n.Y );

                    bool isScrollDown = 
                        gesturePoints.AdjacentMatch((p,n) => p.Y < n.Y );

                    gesturePoints.Clear();

                    if (isScrollDown) this.Content = "Scroll down";
                    else if (isScrollUp) this.Content = "Scroll up";
                    else this.Content = "NOT SCROLLING";

                });


            //mouseDowns
            //    .Subscribe(ep =>
            //    {
                //    Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
                //        .Buffer(mouseUps)
                //        .Subscribe(mmoves =>
                //        {
                //            bool isScrollDown = mmoves
                //                .Select(m => m.EventArgs.GetPosition(this))
                //                .AdjacentMatch((p, n) =>
                //                {
                //                    return p.Y > n.Y;
                //                });

                //            bool isScrollUp = mmoves
                //                .Select(m => m.EventArgs.GetPosition(this))
                //                .AdjacentMatch((p, n) =>
                //                {
                //                    return p.Y < n.Y;
                //                });

                //            if (isScrollDown) this.Content = "Scroll down";
                //            else if (isScrollUp) this.Content = "Scroll up";
                //            else this.Content = "NOT SCROLLING";

                //        });
                //});




            //var query = 
            //from md in mouseDowns
            //from mu in mouseUps
            //let mdPosition = md.EventArgs.GetPosition(this)
            //let muPosition = mu.EventArgs.GetPosition(this)
            //select new Traveller { 
            //    X = muPosition.X - mdPosition.X,
            //    Y = muPosition.Y - mdPosition.Y
            //};

            //query.Subscribe(traveller =>
            //    {
            //        if ( traveller.Y > 0 )
            //        {
            //            this.Content = "Scroll down";
            //        }
            //        else
            //            this.Content = "Scroll up";
            //    });

        }
    }

    static class MyExtensions
    {
        public static bool AdjacentMatch<T>(this IEnumerable<T> source, Func<T, T, bool> match)
        {
            IEnumerator<T> iter = source.GetEnumerator();

            T prev;

            if (iter.MoveNext())
            {
                prev = iter.Current;
            }
            else
            {
                throw new InvalidOperationException("Sequence is empty");
            }

            while (iter.MoveNext())
            {
                if (match(prev, iter.Current))
                {
                    prev = iter.Current;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
