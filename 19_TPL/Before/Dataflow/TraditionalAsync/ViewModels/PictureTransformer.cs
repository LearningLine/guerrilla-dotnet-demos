using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using ImageProcessing;
using MVVM;
using System.Threading;
using System.Windows;
using System.Collections.ObjectModel;

namespace TraditionalAsync.ViewModels
{
    public class PictureTransformer : ViewModel
    {
        private ImageTransforms transforms = new ImageTransforms();

        public PictureTransformer(string directory)
        {
                Images = (Directory.GetFiles(directory,
                "*.jpg", SearchOption.AllDirectories)).ToList();

                ToGrayScale = new DelegateCommand( o => ConvertToGrayScale((string)o) );
                 
              
                Cancel = new DelegateCommand(_ =>
                    {
                        
                    });

        }

        private List<string> images;

        public List<string> Images
        {
            get { return images; }
            set { images = value; OnPropertyChanged("Images"); }
        }

        private ObservableCollection<BitmapSource> transformedImages = new ObservableCollection<BitmapSource>();

        public ObservableCollection<BitmapSource> TransformedImages
        {
            get { return transformedImages; }
        }

        private int imageProcessingProgress;

        public int ImageProcessingProgress
        {
            get { return imageProcessingProgress; }
            set { imageProcessingProgress = value; OnPropertyChanged("ImageProcessingProgress");  }
        }


        public DelegateCommand ToGrayScale { get; private set; }
        public DelegateCommand Cancel { get; private set; }

        private void ConvertToGrayScale(string filename)
        {
             TransformedImages.Add(transforms.CreateGrayScaleImage(new Uri(filename))());
        }

    }
}
