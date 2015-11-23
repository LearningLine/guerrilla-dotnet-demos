using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GroundUpWpf
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Console.WriteLine("Imagine a window...");

            MyApp app = new MyApp();
            app.Run();
        }
    }

    class MyApp : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine("Start this up...");
            var wnd = new MainWindow();
            wnd.Show();
            base.OnStartup(e);
        }
    }

    class MainWindow : Window
    {
        public MainWindow()
        {
            this.Title = "My sexy window";
            
            this.Background = new LinearGradientBrush(
                Colors.Blue, Colors.PaleGoldenrod, 45.0);

            TextBlock tb= new TextBlock();
            tb.Text = "Hello World";
           // tb.FontSize = 72;
            tb.Foreground = Brushes.MistyRose;

            Slider slider = new Slider();
            slider.Width = 200;
            //slider.LayoutTransform = new ScaleTransform(3,3);
            slider.LayoutTransform = new RotateTransform(45);

            Ellipse e1 = new Ellipse();
            e1.Width = 254;
            e1.Height = 100;
            e1.Fill = this.Background;
            e1.Stroke = Brushes.Black;

            //e1.SetValue(Canvas.TopProperty, 200.0);
            //e1.SetValue(Canvas.LeftProperty, 100.0);
            Canvas.SetLeft(e1, 100);
            Canvas.SetTop(e1, 200);
            

            Panel panel = new Canvas();// {Orientation = Orientation.Horizontal};
            panel.Children.Add(slider);
            panel.Children.Add(e1);
            panel.Children.Add(tb);
            this.Content = panel;

        }
        
    }






}
