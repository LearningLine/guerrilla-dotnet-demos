using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ViewModel
{
    public class Order : INotifyPropertyChanged
    {
        public Order()
        {
            items.ListChanged += (s, e) => OnPropertyChanged(null);
        }

        private ObservableCollection<OrderItem> items = new ObservableCollection<OrderItem>();

        private Customer customer;
        public Customer Customer { get { return customer; } set { customer = value; OnPropertyChanged("Customer"); } }

        public ObservableCollection<OrderItem> OrderItems { get { return items; } }

        public decimal NetTotal
        {
            get { return items.Sum( i=>i.NetTotal ); }
        }

        public decimal Total { get { return items.Sum(i => i.Total); } }

        public decimal Tax { get { return NetTotal - Total; } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
