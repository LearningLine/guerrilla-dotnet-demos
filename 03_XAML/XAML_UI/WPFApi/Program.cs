using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFApi
{
    class MyApplication : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var win = new MyWindow();

            win.Show();
            base.OnStartup(e);
        }
    }

    class MyWindow : Window
    {
        public MyWindow()
        {
            Title = "Drab Window?";

            FontSize = 72;

            Background = new LinearGradientBrush(Colors.DarkSlateGray, Colors.DarkTurquoise, 45);

            var text = new TextBlock {Text = "Hello world", Foreground = Brushes.MistyRose};

            var slider = new Slider {Width = 100, LayoutTransform=new RotateTransform(45)};

            var el = new Ellipse {Width = 300, Height = 200, Stroke = Brushes.Violet, Fill = Brushes.MidnightBlue};

            //el.SetValue(Canvas.TopProperty, 100);
            //el.SetValue(Canvas.LeftProperty, 300.0);

            Canvas.SetTop(el, 100);
            Canvas.SetLeft(el, 300);

            // var panel = new StackPanel(){Orientation=Orientation.Horizontal};
            var panel = new Canvas();
            panel.Children.Add(el);


            panel.Children.Add(text);

            Content = panel;


        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new MyApplication();

            app.Run();
        }
    }
}
