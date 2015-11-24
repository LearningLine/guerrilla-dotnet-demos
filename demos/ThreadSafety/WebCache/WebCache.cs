using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Concurrent;

namespace WebCaching
{
    public class WebCache
    {
        private Dictionary<string, string> cache = new Dictionary<string, string>();
       
        public string GetPage(string url)
        {
            string page;

            if (!cache.TryGetValue(url, out page))
            {
                page = LoadPage(url);
                cache.Add(url, page);
            }

            return page;
        }

        private string LoadPage(string url)
        {
            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();
            using(StreamReader reader =new StreamReader(response.GetResponseStream() ))
            {
                return reader.ReadToEnd();
            }
        }

    }
}
