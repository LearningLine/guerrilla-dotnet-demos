using System;
using System.Linq;

namespace _99_MongoDB
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//AddSomeBooks();

			FindAbook();
		}

		private static void FindAbook()
		{
			var ctx = new DataContext();

			var books = ctx.Books.Where(b => b.PageCount > 100).ToList();

			foreach (var b in books)
			{
				Console.WriteLine(b.Title);
				b.Comments.Add(new Comment() {Text = "Soe text", Username="Teddy"});
				b.Comments.Add(new Comment() {Text = "Soe text", Username="Teddy2"});
				b.Comments.Add(new Comment() {Text = "Soe text", Username="Teddy3"});
				ctx.Save(b);
			}
		}

		private static void AddSomeBooks()
		{
			var ctx = new DataContext();

			var book = new Book();
			book.PageCount = 210;
			book.PublishedDate = DateTime.Now;
			book.Title = "C# + MongoDB = <3 II";

			ctx.Save(book);
		}
	}
}