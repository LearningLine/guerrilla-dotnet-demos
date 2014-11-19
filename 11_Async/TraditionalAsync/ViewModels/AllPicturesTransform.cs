using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ImageProcessing;

namespace TraditionalAsync.ViewModels
{
    public class AllPicturesTransform
    {

        private ImageTransforms transforms = new ImageTransforms();

        public AllPicturesTransform(string directory)
        {
            TransformedImages =new ObservableCollection<BitmapSource>();

            AllPicturesToGrayScale(directory);

        }

        private async void AllPicturesToGrayScale(string directory)
        {
            var colourImages = Directory.GetFiles(directory, "*.jpg", SearchOption.AllDirectories)
                .Where(fn => !fn.Contains("bad"))
                .Select(fn => new Uri(fn))
                // .Take(2)
                .ToList();

            var tasks = new List<Task<BitmapSource>>();

            foreach (Uri colourImage in colourImages)
            {
                tasks.Add(transforms.CreateGrayScaleImageAsync(colourImage));
                //TransformedImages.Add(await transforms.CreateGrayScaleImageAsync(colourImage));
            }

            //await Task.WhenAll(tasks);
            //foreach (var t in tasks)
            //{
            //    TransformedImages.Add(await t);
            //}

            // results in n * (n+1) / 2 continuations.
            //while (tasks.Count > 0)
            //{
            //    Task<BitmapSource> task = await Task.WhenAny(tasks);
            //    tasks.Remove(task);
            //    TransformedImages.Add(task.Result);
            //}

            foreach (var t in tasks.InOrder())
            {
                TransformedImages.Add(await t);
            }

        }

      

        public ObservableCollection<BitmapSource> TransformedImages
        {
            get;
            private set;
        }
    }

    
}