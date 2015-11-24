using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AmazonLite.Models;

namespace AmazonLite.Controllers
{
    //[RoutePrefix("/")]
    public class BooksController : ApiController
    {
        static List<Book> books = new List<Book>()
        {
            new Book() { ISBN = "AU2",Title = "Robin Hood"},
            new Book() { ISBN = "AU3",Title = "Fight Club"},
            new Book() { ISBN = "GZ2",Title = "Little Red Riding Hood"},
            new Book() { ISBN = "GZ1",Title = "Forensics for Dummies"},
        };

        [Route("books")]
        [HttpGet]
        public IEnumerable<Book> ShowMeTheBooks()
        {
            return books;
        }

        [Route("book/{isbn}")]
        [HttpGet]
        public IHttpActionResult GetBookByIsbn(string isbn)
        {
            var book = books.SingleOrDefault(b => b.ISBN == isbn);
            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}
