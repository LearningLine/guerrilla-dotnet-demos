using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PictureLibrary;

namespace SurfaceLite
{
    public partial class MainWindow : Window
    {
        private WebPageImageExtractor extractor = new WebPageImageExtractor();
        private Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            extractor.NewImage += Extractor_NewImage;
        }

        private void Extractor_NewImage(object sender, ImageEventArgs e)
        {
            BitmapImage imgSrc = new BitmapImage(e.Uri);
            Image img = new Image();
            img.Source = imgSrc;
            img.Width = 200;
            Canvas.SetLeft(img, rnd.NextDouble()*surface.ActualWidth);
            Canvas.SetTop(img, rnd.NextDouble()*surface.ActualHeight);
            img.RenderTransform = new RotateTransform(rnd.Next(-20,20));

            surface.Children.Add(img);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            string uri = this.uriTextBox.Text;
            extractor.FindImagesAsync(new Uri(uri));
        }
    }
}
