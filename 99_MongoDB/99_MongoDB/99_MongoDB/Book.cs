using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Kennedy;

namespace _99_MongoDB
{
	public class Book : IMongoEntity
	{
		private List<Comment> _comments;
		public ObjectId _id { get; private set; }
		public string _accessId { get; set; }

		public string Title { get; set; }
		public int PageCount { get; set; }
		public DateTime PublishedDate { get; set; }
		public string Publisher { get; set; }

		public List<Comment> Comments
		{
			get
			{
				if (_comments == null)
					_comments = new List<Comment>();
				return _comments;
			}
			set { _comments = value; }
		}
	}

	public class Comment
	{
		public string Username { get; set; }
		public string Text { get; set; }
	}
}
