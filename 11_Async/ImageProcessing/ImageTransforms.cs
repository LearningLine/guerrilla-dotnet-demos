using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ImageTransforms
    {
         public Task<BitmapSource> CreateGrayScaleImageAsync(string uri)
         {
             return CreateGrayScaleImageAsync(new Uri(uri));
         }

        public Task<BitmapSource> CreateGrayScaleImageAsync(Uri uri)
        {
            return Task.Run<BitmapSource>(() => CreateGrayScaleImage(uri));
        }

        public BitmapSource CreateGrayScaleImage(string uri)
        {
            return CreateGrayScaleImage(new Uri(uri));
        }

        public BitmapSource CreateGrayScaleImage(Uri uri)
        {
            var img = new BitmapImage(uri);

            return CreateGrayScaleImage(img);
        }

        public BitmapSource CreateGrayScaleImage(BitmapSource img)
        {
            var width = (int)img.Width;
            var height = (int)img.Height;
            double dpi = img.DpiX;

            var pixelData = new byte[(int)img.Width * (int)img.Height * 4];
            var outputData = new byte[width * height];

            img.CopyPixels(pixelData, (int)img.Width * 4, 0);

            int totalIterations = 5*width;

            for (int i = 0; i < 5; i++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    for (int y = 0; y < img.Height; y++)
                    {
                       
                        byte red = pixelData[y*(int) img.Width*4 + x*4 + 1];
                        byte green = pixelData[y*(int) img.Width*4 + x*4 + 2];
                        byte blue = pixelData[y*(int) img.Width*4 + x*4 + 3];

                        var gray = (byte) (red*0.299 + green*0.587 + blue*0.114);

                        outputData[y*width + x] = gray;
                    }
                }
            }

            BitmapSource greyImage =  BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Gray8, null, outputData, width);
            greyImage.Freeze();

            return greyImage;
        }
    }
}
