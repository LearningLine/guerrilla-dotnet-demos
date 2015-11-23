using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLibrary;

namespace TestPictureLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var imageService = new WebPageImageExtractor();


            imageService.NewImage += (s, e) =>
            {
                Console.WriteLine(e.Uri);
            };


            imageService.FindImagesAsync(new Uri("http://www.bbc.co.uk"));

            Console.ReadLine();
        }
    }
}
