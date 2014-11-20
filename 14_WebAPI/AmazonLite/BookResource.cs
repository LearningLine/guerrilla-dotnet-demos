using Hypermedia;

namespace AmazonLite
{
    public class BookResource : Resource
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
