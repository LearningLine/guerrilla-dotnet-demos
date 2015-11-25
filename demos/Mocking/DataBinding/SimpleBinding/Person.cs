using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Media;
using SimpleBinding.Annotations;

namespace SimpleBinding
{

    class Person : INotifyPropertyChanged
    {
        private int age;
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged();
                OnPropertyChanged("AgeColour");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        public override string ToString()
        {
            return String.Format("{0} aged {1}", Name, Age);
        }

        //public Brush AgeColour
        //{
        //    get
        //    {
        //        if (Age < 22)
        //        {
        //            return Brushes.Yellow;
        //        }
        //        else if (Age < 40)
        //        {
        //            return Brushes.Violet;
        //        }
        //        else
        //        {
        //            return Brushes.Gray;
        //        }
        //    }
        //}


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}


