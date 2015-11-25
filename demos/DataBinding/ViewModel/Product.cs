using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
    }
}
