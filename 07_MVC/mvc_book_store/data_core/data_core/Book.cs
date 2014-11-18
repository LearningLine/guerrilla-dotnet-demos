using System;
using System.Collections.Generic;

namespace Amazoon.Core.Models
{
	public class Book 
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public float Price { get; set; }

		public int CategoryId { get; set; }

	}
}
