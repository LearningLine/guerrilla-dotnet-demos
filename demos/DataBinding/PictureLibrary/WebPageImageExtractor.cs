using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PictureLibrary
{
    public class WebPageImageExtractor
    {

        public event EventHandler<ImageEventArgs> NewImage = delegate { };
      

        public async void FindImagesAsync(Uri webPage)
        {
            foreach (Uri image in await GetPageImages(webPage))
            {
                NewImage(this,new ImageEventArgs(image));         
            }
        }

        private static async Task<List<Uri>> GetPageImages(Uri webPage)
        {

            var client = new WebClient();

            string searchResults = await client.DownloadStringTaskAsync(webPage).ConfigureAwait(false);

            List<Uri> results = searchResults.Split('"')
                .Select(p => p.ToLower())
                .Where(p => p.EndsWith("jpg") || p.EndsWith("png"))
                .Select(p => new Uri(p, UriKind.RelativeOrAbsolute))
                .Select(u => u.IsAbsoluteUri ? u : new Uri(webPage, u))
                .ToList();
            return results;
        }

    }
}