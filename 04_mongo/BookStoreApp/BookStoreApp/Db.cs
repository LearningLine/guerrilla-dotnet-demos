using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreApp
{
	class Db : MongoDB.Kennedy.MongoDbDataContext
	{
		public IQueryable<Book> Books { get { return base.GetCollection<Book>(); } }


		public Db() : base("GNETBooks")
		{
		}
	}

	//[BsonIgnoreExtraElements]
	class Book
	{
		public ObjectId Id { get; set; }

		public string Title { get; set; }
		public int PageCount { get; set; }
		public bool IsPrint { get; set; }

		public List<Review> Reviews { get; set; }

		[BsonExtraElements]
		public BsonDocument AddtionalData { get; set; }

		public Book()
		{
			IsPrint = true;
			Reviews = new List<Review>();
		}
	}

	internal class Review
	{
		public DateTime Date { get; set; }
		public string Comment { get; set; }
		public int Stars { get; set; }
		public string UserName { get; set; }

		public Review()
		{
			Date = DateTime.UtcNow;
		}
	}
}















