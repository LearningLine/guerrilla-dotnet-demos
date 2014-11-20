using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using BookService;
using Hypermedia;

namespace AmazonLite.Controllers
{
    public class BooksController : ApiController
    {
        BookRepository repository = new BookRepository();
        
        [HttpGet]
        [Route("books", Name = "ListOfBooks")]
        public IEnumerable<Book> Get()
        {
            return repository.GetBooks();
        }

        [HttpGet]
        [Route("books/{isbn}", Name = "BookByISBN")]
        public IHttpActionResult Get(string isbn )
        {
            Book book = repository.GetBookByISBN(isbn);
            if (book == null)
            {
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);    
                //return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Ok(book.ToResource(Request)); //book; //Request.CreateResponse(HttpStatusCode.OK, book);
        }
    }

    static class ResourceExtensions
    {
        public static BookResource ToResource(this Book book, HttpRequestMessage request)
        {
            BookResource br = new BookResource();
            br.ISBN = book.ISBN;
            br.ImageUrl = book.ImageUrl;
            br.Price = book.Price;
            br.Title = book.Title;

            var urlHelper = new UrlHelper(request);
            var routeValues = new {isbn = book.ISBN};
            string link = urlHelper.Link("BookByISBN", routeValues);
            br.Links.Add( new Link() {Relationship = Relationship.Self, Verb = "GET", Uri = link});
            br.Links.Add(new Link() { Relationship = Relationship.Parent, Verb = "GET", Uri = urlHelper.Route("ListOfBooks", new object()) });

            return br;
        }
    }
}