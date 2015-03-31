using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;

namespace BookStoreApp
{
	class Program
	{
		static void Main(string[] args)
		{
			//CreateData();
			QueryData();
		}

		private static void QueryData()
		{
			var db = new Db();

			var longBooks =
				from b in db.Books
				where b.PageCount >= 500
				orderby b.PageCount descending
				select b;

			Console.WriteLine("The query is:");
			Console.WriteLine(longBooks.ToMongoQueryText());

			foreach (var b in longBooks)
			{
				Console.WriteLine("{0} with {1:N0} pages", b.Title, b.PageCount);
			}

		}

		private static void CreateData()
		{
			var db = new Db();


			Book b = new Book();
			b.Title = "Fancy db programming v1";
			b.PageCount = 201;
			b.Reviews.Add(new Review() {Comment = "Loved it!", Stars = 5});
			b.Reviews.Add(new Review() {Comment = "Meh", Stars = 3});

			db.Save(b);

			 b = new Book();
			b.Title = "LOLCat programming callenges";
			b.PageCount = 1001;

			db.Save(b);
		}
	}
}
