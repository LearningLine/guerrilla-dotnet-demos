using System;
using System.Collections.Generic;

namespace Amazoon.Core.Models
{
	public class Comment
	{
		// 1. Add two properties (not fields):
		// string Username and string CommentText.
		// We do not an ObjectId _id primary key because
		// we never directly work with this type. It's nested
		// in the Book class.
		public string Username { get; set; }
		public string CommentText { get; set; }
		public List<int> Votes { get; set; }

		public Comment()
		{
			Votes = new List<int>();
		}
	}
}
