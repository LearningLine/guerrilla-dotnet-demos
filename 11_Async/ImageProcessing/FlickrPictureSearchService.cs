using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImageProcessing
{
    public interface IPictureSearchService
    {
        Task<IList<Uri>> SearchAsync(string term, int nMatches);
    }

    public class FlickrPictureSearchService : IPictureSearchService
    {
        public async Task<IList<Uri>> SearchAsync(string term, int nMatches)
        {
            WebClient client = new WebClient();

            string searchUrl = String.Format("http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=b2f4597ab86d10fd6fbca0a6ed82f3f1&tags={0}&per_page={1}",
                term, nMatches);


            string searchResults = await client.DownloadStringTaskAsync(searchUrl);
                

                XElement response = XElement.Parse(searchResults);

                IList<Uri> results = (from photo in response.Descendants("photo")
                                      select new Uri(String.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}.jpg",
                                                           (string)photo.Attribute("farm"),
                                                           (string)photo.Attribute("server"),
                                                           (string)photo.Attribute("id"),
                                                           (string)photo.Attribute("secret")))).ToList();


            return results;
        } 
    }
}