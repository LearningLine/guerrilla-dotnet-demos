using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Buffering
{
    class Combiner : INotifyPropertyChanged
    {
        private int val;
        public int Value 
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
