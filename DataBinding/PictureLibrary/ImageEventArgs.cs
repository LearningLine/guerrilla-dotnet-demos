using System;
using System.Text;
using System.Threading;

namespace PictureLibrary
{
    public class ImageEventArgs : EventArgs
    {
        public ImageEventArgs(Uri url)
        {
            Uri = url;
        }

        public Uri Uri { get; private set; }
    }
}
