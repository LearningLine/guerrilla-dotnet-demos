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


namespace RxEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (from ev in Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
                let pos = ev.EventArgs.GetPosition(this).X
                where pos < 300
                select pos.ToString())
                .Subscribe(xAsString => myText.Text = xAsString);

            //Observable
            //    .FromEventPattern<MouseEventArgs>(this, "MouseMove")
            //    .Where(mouseMoveEvent => mouseMoveEvent.EventArgs.GetPosition(this).X < 300 )
            //    .Subscribe(mouseMoveEvent =>
            //    {
            //        myText.Text = mouseMoveEvent
            //            .EventArgs
            //            .GetPosition(this).X.ToString();
            //    });

        }
    }
}







