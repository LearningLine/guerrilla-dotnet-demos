using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
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

        private  void AllPicturesToGrayScale(string directory)
        {
            var colourImages = Directory.GetFiles(directory,"*.jpg", SearchOption.AllDirectories)
                .Where(fn => !fn.Contains("bad") )
                .Select(fn => new Uri(fn))
                .Take(2)
                .ToList();

            foreach (Uri colourImage in colourImages)
            {
                TransformedImages.Add(transforms.CreateGrayScaleImage(colourImage));
            }
        }

      

        public ObservableCollection<BitmapSource> TransformedImages
        {
            get;
            private set;
        }
    }

    
}