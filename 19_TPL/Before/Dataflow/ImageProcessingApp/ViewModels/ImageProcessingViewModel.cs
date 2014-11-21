using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImageProcessing;
using MVVM;

namespace ImageProcessingApp.ViewModels
{

    public abstract class FileSystemElement
    {
        protected readonly string path;

        public FileSystemElement(string path)
        {
            this.path = path;
            Name = System.IO.Path.GetFileName(path);
        }

        public string Name { get; protected set; }
        public string Path { get { return path; } }

        public abstract IEnumerable<FileSystemElement> Children { get; }
    }

    public class ImageFile : FileSystemElement
    {
        public ImageFile(string path) : base(path)
        {
        }

        public override IEnumerable<FileSystemElement> Children
        {
            get { yield break; }
        }

        
    }
    public class Folder : FileSystemElement
    {
        

        public Folder(string path) : base(path)
        {
        }



        public IEnumerable<Folder> Folders
        {
            get
            {
                return new DirectoryInfo(path)
                    .GetDirectories()
                    .Select(d => new Folder(d.FullName));
            }
        }
        public IEnumerable<ImageFile> Images {
            get
            {
                return new DirectoryInfo(path)
                   .GetFiles()
                   .Where(fi => fi.Extension == ".jpg" || fi.Extension == ".png")
                   .Select(fi => new ImageFile(fi.FullName));
            }
        }

        public override IEnumerable<FileSystemElement> Children
        {
            get { return Folders.Concat<FileSystemElement>(Images); }
        }
    }

    public class ImageProcessingViewModel : ViewModel
    {
        private ImageTransforms imageTransforms = new ImageTransforms();
        private DelegateCommand addImage;

        public ImageProcessingViewModel(string path)
        {
            Root = new Folder(path);
            addImage = new DelegateCommand(image => AddImage((FileSystemElement)image));
            TransformedImages = new ObservableCollection<BitmapSource>();
        }

        public ObservableCollection<BitmapSource> TransformedImages { get; set; } 

        private void AddImage(FileSystemElement image)
        {
         
            BitmapSource grayImage = imageTransforms.CreateGrayScaleImage(image.Path);

            TransformedImages.Add(grayImage);
            return;
        }

        public ICommand Add { get { return addImage; } }
        public Folder Root { get; private set; }
    }
}