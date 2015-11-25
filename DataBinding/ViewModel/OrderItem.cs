using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ViewModel
{
    public class OrderItem : INotifyPropertyChanged
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Tax { get; set; }

        private int quantity = 0;
        public int Quantity { get { return quantity; } set { quantity = value; OnPropertyChanged(null); } }

        public decimal Total { get { return Quantity * UnitPrice; } }
        public decimal NetTotal { get { return Total + Total * Tax; } }

    
        public event PropertyChangedEventHandler  PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null )
            {
                PropertyChanged( this , new PropertyChangedEventArgs(property) );
            }
        }

        private bool hasError;
        public bool HasError { get { return hasError; } set { hasError = value; OnPropertyChanged("HasError"); } }
    }
}
