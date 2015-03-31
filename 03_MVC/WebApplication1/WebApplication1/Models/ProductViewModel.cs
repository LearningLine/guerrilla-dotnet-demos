using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Range(0, Double.MaxValue, ErrorMessage="Price must be zero or greater")]
        public decimal Price { get; set; }
    }
}