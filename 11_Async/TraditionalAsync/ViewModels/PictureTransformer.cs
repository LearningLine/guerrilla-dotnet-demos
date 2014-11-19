using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageProcessing;
using MVVM;

namespace TraditionalAsync.ViewModels
{
    public class PictureTransformer : ViewModel
    {
        private readonly ObservableCollection<BitmapSource> transformedImages = new ObservableCollection<BitmapSource>();
        private readonly ImageTransforms transforms = new ImageTransforms();
        private int imageProcessingProgress;

        private List<string> images;

        private string status;

        public PictureTransformer(string directory)
        {
            Images = (Directory.GetFiles(directory,
                "*.jpg", SearchOption.AllDirectories)).ToList();

            ToGrayScale = new DelegateCommand(async o =>
            {
                try
                {
                    Status = "Converting " + o.ToString();
                    await ConvertToGrayScale((string) o);
                    Status = "Converted " + o.ToString();
                }
                catch (Exception error)
                {
                    Status = "Failed " + error.Message;
                }
            });
        }

        public List<string> Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChanged("Images");
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }


        public ObservableCollection<BitmapSource> TransformedImages
        {
            get { return transformedImages; }
        }

        public int ImageProcessingProgress
        {
            get { return imageProcessingProgress; }
            set
            {
                imageProcessingProgress = value;
                OnPropertyChanged("ImageProcessingProgress");
            }
        }


        public DelegateCommand ToGrayScale { get; private set; }

        private async Task ConvertToGrayScale(string filename)
        {
            BitmapSource greyImage = await transforms.CreateGrayScaleImageAsync(filename);
            TransformedImages.Add(greyImage);
        }

        //private  void ConvertToGrayScale(string filename)
        //{
        //    Task<BitmapSource> grayScaleImageTask = transforms.CreateGrayScaleImageAsync(filename);

        //    grayScaleImageTask.ContinueWith(t =>
        //    {
        //        BitmapSource greyImage = t.Result;
        //        TransformedImages.Add(greyImage);
        //    }, TaskScheduler.FromCurrentSynchronizationContext());
        //}

        //private  void ConvertToGrayScale(string filename)
        //{
        //    BitmapSource greyImage = transforms.CreateGrayScaleImage(filename);
        //    TransformedImages.Add(greyImage);
        //}
    }
}