using System;
using System.Windows.Media.Imaging;

namespace ImageProcessing
{
    public class ImageTransformCompletedEventArgs : EventArgs
    {

        public ImageTransformCompletedEventArgs(Exception e)
        {
            Error = e;
        }

        public ImageTransformCompletedEventArgs(BitmapSource result)
        {
            Result = result;
        }

        public Exception Error { get; private set; }
        public BitmapSource Result { get; private set; }

    }
}