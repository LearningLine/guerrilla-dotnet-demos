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

            //Observable
            //    .FromEventPattern<MouseEventArgs>(this, "MouseMove")
            //    .Subscribe(ev =>
            //    {
            //        var point = ev.EventArgs.GetPosition(this);
            //        myText.Text = point.X.ToString();
            //    });

            (from ev in Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
             let x = ev.EventArgs.GetPosition(this).X
             where x > 300
            select x).Subscribe(x =>{myText.Text = x.ToString();});



        }
    }
}







