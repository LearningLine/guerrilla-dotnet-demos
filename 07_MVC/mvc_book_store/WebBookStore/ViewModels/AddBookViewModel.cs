using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class AddBookViewModel
    {
        [Required()]
        public string Name { get; set; }
        [Required]
        [Range(0,1000)]
        public float Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }


    }
}