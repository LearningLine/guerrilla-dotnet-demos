using System.Collections.Generic;
using System.Web.Mvc;
using Amazoon.Core.Models;
using data_core;
using WebBookStore.ViewModels;

namespace WebBookStore.Controllers
{
    public class BooksController:Controller
    {

        // Books/Category/{id}
        public ActionResult Category(int id)
        {
            var bookRepository = new BookRepository();

            
            IEnumerable<Book> model = bookRepository.BooksByCategory(id);

            return View(model);
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddBook(AddBookViewModel newBook)
        {

            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var bookRepository = new BookRepository();

            bookRepository.AddBook(new Book()
            {
                Name =  newBook.Name,
                CategoryId = 1,
                ImageUrl =  newBook.ImageUrl,
                Price = newBook.Price
            });
            return Redirect("/Books/Category/1");
        }
         
    }
}