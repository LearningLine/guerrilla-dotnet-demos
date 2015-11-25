using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            
            Images = new ObservableCollection<BitmapSource>();

            Search = new DelegateCommand(_ => SearchForImages(SearchTerm));
        }

        private async void SearchForImages(string searchTerm)
        {
            WebClient client = new WebClient();

            Uri termUri = new Uri(searchTerm);

            var results = await GetResultsAsync(searchTerm, client, termUri);

            foreach (Uri uri in results)
            {
                BitmapImage img = new BitmapImage(uri);

                Images.Add(img);
            }

        }

        private static async Task<List<Uri>> GetResultsAsync(string searchTerm, WebClient client, Uri termUri)
        {
            string searchResults = await client.DownloadStringTaskAsync(searchTerm)
                .ConfigureAwait(continueOnCapturedContext: false);

            List<Uri> results = searchResults.Split('"')
                .Select(p => p.ToLower())
                .Where(p => p.EndsWith("jpg") || p.EndsWith("png"))
                .Select(p => new Uri(p, UriKind.RelativeOrAbsolute))
                .Select(u => u.IsAbsoluteUri ? u : new Uri(termUri, u))
                .ToList();

            return results;
        }


        public ObservableCollection<BitmapSource> Images { get; set; }
        public string SearchTerm { get; set; }
        public ICommand Search { get; private set; }

        
    }
}