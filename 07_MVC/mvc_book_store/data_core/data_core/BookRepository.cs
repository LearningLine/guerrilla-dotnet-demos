using System.Collections.Generic;
using System.Linq;
using Amazoon.Core.Models;

namespace data_core
{
	public class BookRepository
	{
		private static readonly List<Category> categories = new List<Category>
		{
			new Category
			{
				Id = 1,
				Name = "Programming",
				ImageUrl = "http://www.securitygeneration.com/wp-content/gallery/random/lolcat-internets.jpg"
			},
			new Category
			{
				Id = 2,
				Name = "Robotics",
				ImageUrl = "http://i477.photobucket.com/albums/rr137/bobblob23/1303596402073.jpg"
			},
			new Category
			{
				Id = 3,
				Name = "Math",
				ImageUrl = "http://i55.photobucket.com/albums/g160/catgirlpix/pythagoreancat.jpg"
			},
		};

		private static readonly List<Book> books = new List<Book>
		{
			new Book
			{
				Id = 1,
				CategoryId = 1,
				Name = "HTML5 for cats",
				Price = 19.99f,
				ImageUrl = "http://inceptor.mcs.suffolk.edu/~tristam/cs120/images/lolcat.jpg"
			},
			new Book
			{
				Id = 2,
				CategoryId = 1,
				Name = "99.9% Uptime",
				Price = 29.99f,
				ImageUrl = "http://xspblog.files.wordpress.com/2008/06/funny-pictures-cats-computer-blue-screen-death.jpg"
			},
		};

		public IEnumerable<Category> AllCategories()
		{
			return categories;
		}

		public IEnumerable<Book> BooksByCategory(int categoryId)
		{
			return from b in books
				where b.CategoryId == categoryId
				select b;
		}

		public void AddBook(Book book)
		{
			int index = books.FindIndex(b => b.Id == book.Id);
			if (index >= 0)
			{
				books[index] = book;
			}
			else
			{
				books.Add(book);
			}
		}

		public void Commit()
		{
		}
	}
}