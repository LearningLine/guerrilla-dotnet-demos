using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;
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
using Payments;

namespace TestSwipe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Observable.FromEventPattern<TextCompositionEventArgs>(this, "PreviewTextInput")
                .TakeWhile(ep => ep.EventArgs.Text != "\r")
                .Timeout(TimeSpan.FromSeconds(10))
                .Subscribe(ep => builder.Append(ep.EventArgs.Text),
                x => MessageBox.Show("timeout"), DecodeDetails);

          //  this.PreviewTextInput += OnKeyDown;
        }

        private readonly StringBuilder builder = new StringBuilder();
        private void OnKeyDown(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "\r")
            {
                builder.Append(e.Text);
            }
            else
            {
                DecodeDetails();
            }
        }

        private void DecodeDetails()
        {
            string raw = builder.ToString();

            var decoder = new SwipeCardDecoder();

            try
            {
                CardDetails details = decoder.Decode(raw);
                details.Validate();

                number.Text = details.Number;
                name.Text = details.Name;
                month.Text = details.ExpiryMonth;
                year.Text = details.ExpiryYear;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


            builder.Clear();
        }

        private static string GetYear(string raw)
        {
            int firstSep = raw.IndexOf("^");
            int secondSep = raw.IndexOf("^", firstSep + 1);
            return raw.Substring(secondSep + 1, 2);
        }

        private static string GetMonth(string raw)
        {
            int firstSep = raw.IndexOf("^");
            int secondSep = raw.IndexOf("^", firstSep + 1);
            return raw.Substring(secondSep + 3, 2);
        }

        private static string GetName(string raw)
        {
            int firstSep = raw.IndexOf("^");
            int secondSep = raw.IndexOf("^", firstSep + 1);
            return raw.Substring(firstSep + 1, secondSep - firstSep - 1).Trim();
        }

        private static string GetNumber(string raw)
        {
            int firstSep = raw.IndexOf("^");
            return raw.Substring(2, firstSep - 2);
        }
    }
}
