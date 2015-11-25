using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using MVVM;
using PictureLibrary;

namespace WpfTheRightWay.ViewModels
{
    public class PictureViewModel : ViewModel
    {
        private string url;
        private WebPageImageExtractor imageExtractor = new WebPageImageExtractor();

        public PictureViewModel()
        {
            Url = "http://www.bbc.co.uk";
            Images = new ObservableCollection<Uri>();

            imageExtractor.NewImage += (s, e) =>
            {
                Images.Add(e.Uri);
            };

            GoCommand = new DelegatingCommand(() =>
            {
                imageExtractor.FindImagesAsync(new Uri(Url));

                Url = "http://";
            });
        }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Uri> Images { get; set; }


        public DelegatingCommand GoCommand { get; private set; }
    }


}