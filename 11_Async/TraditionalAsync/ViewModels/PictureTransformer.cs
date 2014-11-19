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

                ToGrayScale = new DelegateCommand(  o =>
                    {
                        try
                        {
                            Status = "Converting " + o.ToString();
                            ConvertToGrayScale((string) o);
                            Status = "Converted " + o.ToString();
                        }
                        catch (Exception error)
                        {
                            Status = "Failed " + error.Message;
                        }
                    } );
                
        }

        private List<string> images;

        public List<string> Images
        {
            get { return images; }
            set { images = value; OnPropertyChanged("Images"); }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status");}
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
       

        private  void ConvertToGrayScale(string filename)
        {
         
            BitmapSource greyImage = transforms.CreateGrayScaleImage(filename);

            TransformedImages.Add(greyImage);
         
        }

    }
}
