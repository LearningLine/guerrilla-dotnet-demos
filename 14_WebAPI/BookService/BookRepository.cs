using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    public class BookRepository
    {
        private static List<Book> books = new List<Book>
        {
            new Book{ISBN="978-1430259206", Title="Pro Async Programming with .NET", Price = 32.50m, ImageUrl = "http://ecx.images-amazon.com/images/I/517V7hgPTRL._SL500_PIsitb-sticker-arrow-big,TopRight,35,-73_OU02_SS100_.jpg"},
            new Book{ISBN="978-0007428540", Title="A Game of Thrones", Price = 3.85m, ImageUrl = "http://ecx.images-amazon.com/images/I/51Vb90Q1SlL._SX60_CR,0,0,60,60_.jpg"},
            new Book{ISBN="978-0007525492", Title="The Hobbit", Price = 3.85m, ImageUrl = "http://ecx.images-amazon.com/images/I/71j6LlCnuEL._SX75_CR,0,0,75,75_.jpg"},
            new Book{ISBN="978-0224101998", Title="The Children's Act", Price = 6.95m, ImageUrl = "http://ecx.images-amazon.com/images/I/4180MTPOaiL._SX60_CR,0,0,60,60_.jpg"},
        };

        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        public void DeleteBook(string isbn)
        {
            books.RemoveAll(b => b.ISBN == isbn);
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public Book GetBookByISBN(string isbn)
        {
            return books.SingleOrDefault(b => b.ISBN == isbn);
        }
    }

    public class Book 
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
