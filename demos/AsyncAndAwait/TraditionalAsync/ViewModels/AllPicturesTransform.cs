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

        private async Task AllPicturesToGrayScale(string directory)
        {
            var colourImages = Directory.GetFiles(directory,"*.jpg", SearchOption.AllDirectories)
                .Where(fn => !fn.Contains("bad") )
                .Select(fn => new Uri(fn))
                //.Take(2)
                .ToList();

            List<Task<BitmapSource>> tasks = new List<Task<BitmapSource>>();

            foreach (Uri colourImage in colourImages)
            {
                tasks.Add(transforms.CreateGrayScaleImageAsync(colourImage));
            }

            //while (tasks.Count > 0)
            //{
            //    Task<BitmapSource> completedTask = await Task.WhenAny(tasks);

            //    TransformedImages.Add(completedTask.Result);

            //    tasks.Remove(completedTask);
            //}
          //  await Task.WhenAll(tasks);

            foreach (Task<BitmapSource> task in tasks.AsCompletingOrder())
            {
                TransformedImages.Add(await task);
            }
        }



        public ObservableCollection<BitmapSource> TransformedImages
        {
            get;
            private set;
        }


    }

    static class TaskExtensions
    {
        public static IEnumerable<Task<T>> AsCompletingOrder<T>(this IEnumerable<Task<T>> source)
        {
            Task<T>[] tasksToObserve = source.ToArray();
            TaskCompletionSource<T>[] tcss = new TaskCompletionSource<T>[tasksToObserve.Length];

            int nResult = -1;
            for (int i = 0; i < tasksToObserve.Length; i++)
            {
                tcss[i] = new TaskCompletionSource<T>();
                tasksToObserve[i].ContinueWith(prev =>
                {
                    int currentResult = Interlocked.Increment(ref nResult);

                    tcss[currentResult].SetResult(prev.Result);
                });
            }

            return tcss.Select(tcs => tcs.Task);
        } 
    }

    
}