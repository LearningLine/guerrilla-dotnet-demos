using System;

namespace _17_csharp6
{
	public class Product
	{
		public int Id { get; set; }
		public double Price { get; private set; }
		public string Name { get; set; }

		public Product(int id, double price, string productName)
		{
			if (string.IsNullOrWhiteSpace(productName))
			{
				throw new ArgumentNullException(nameof(productName));
			}
			this.Id = id;
			this.Price = price;
			this.Name = productName;
		}
	}
}