using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using ImageProcessing;
using MVVM;

namespace TraditionalAsync.ViewModels
{
    public class PictureSearch : ViewModel
    {      
        public PictureSearch()
        {
            SearchTerm = "http://www.develop.com";
            Images = new ObservableCollection<BitmapSource>();

            Search = new DelegateCommand(_ => SearchForImages(SearchTerm));
        }

        private async void SearchForImages(string searchTerm)
        {
            WebClient client = new WebClient();

            Uri termUri = new Uri(searchTerm);

            string searchResults = await client.DownloadStringTaskAsync(searchTerm);

            List<Uri> results = searchResults.Split('"')
                .Select(p => p.ToLower())
                .Where(p => p.EndsWith("jpg") || p.EndsWith("png"))
                .Select(p => new Uri(p, UriKind.RelativeOrAbsolute))
                .Select(u => u.IsAbsoluteUri ? u : new Uri(termUri, u))
                .ToList();

            foreach (Uri uri in results)
            {
                BitmapImage img = new BitmapImage(uri);

                Images.Add(img);
            }

        }


        public ObservableCollection<BitmapSource> Images { get; set; }
        public string SearchTerm { get; set; }
        public ICommand Search { get; private set; }

        
    }
}