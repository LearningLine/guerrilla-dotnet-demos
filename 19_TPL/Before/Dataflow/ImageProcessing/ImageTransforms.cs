using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ImageTransforms
    {
        private TransformBlock<string, BitmapSource> toGrayBlock;
        private ActionBlock<BitmapSource> publishBlock;
        private TransformManyBlock<string, string> directoryBlock;
        public event EventHandler<ImageTransformCompletedEventArgs> ImageTransformed = delegate { };

        public ImageTransforms()
        {
            this.toGrayBlock = new TransformBlock<string, BitmapSource>(file =>
            {
                BitmapSource source = CreateGrayScaleImageSafe(file);
                return source;
            }, new ExecutionDataflowBlockOptions() {MaxDegreeOfParallelism = 4});

            this.publishBlock = new ActionBlock<BitmapSource>(source =>
            {
                ImageTransformed(this, new ImageTransformCompletedEventArgs(source));
            }, new ExecutionDataflowBlockOptions() {TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()});

            this.directoryBlock = new TransformManyBlock<string, string>(directory =>
            {
                DirectoryInfo di = new DirectoryInfo(directory);
                return di.GetFiles("*.jpg").Select(fi => fi.FullName)
                    .Concat(di.GetDirectories().Select(sdi => sdi.FullName));
            });

            toGrayBlock.LinkTo(publishBlock);
            directoryBlock.LinkTo(directoryBlock, file => Directory.Exists(file));
            directoryBlock.LinkTo(toGrayBlock);


            toGrayBlock.Completion.ContinueWith(t =>
            {
                MessageBox.Show(t.Exception.InnerExceptions.First().Message);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public void CreateGrayScaleImageAsync(string path)
        {
            if (Directory.Exists(path))
            {
                this.directoryBlock.Post(path);
            }
            else
            {
                this.toGrayBlock.Post(path);
            }
        }

        public BitmapSource CreateGrayScaleImageSafe(string path)
        {
            try
            {
                return CreateGrayScaleImage(path);
            }
            catch (Exception)
            {
                Debug.WriteLine("That was too bad! ");
                return null;
            }
        }


        public  BitmapSource CreateGrayScaleImage(string path )
        {
            var img = new BitmapImage(new Uri(path));

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

            var greyBitmap =
                BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Gray8, null, outputData, width);

            greyBitmap.Freeze();

            return greyBitmap;


        }


      
    }
}
