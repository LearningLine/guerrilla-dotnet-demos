using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17_csharp6
{
	public class Order
	{
		public int Id { get; }
		public DateTime Created { get;  } = DateTime.Now;
		public List<Product> Products { get; set; } = new List<Product>();

		public Order(int id)
		{
			this.Id = id;
		}

		public double Total => Products.Sum(p => p.Price);

		public override string ToString() =>
			"Order: \{Products.Count} items for $\{Total}";
				
	}


	//public class Order
	//{
	//	public int Id { get; set; }
	//	public List<Product> Products { get; set; }

	//	public Order(int id)
	//	{
	//		Products = new List<Product>();
	//	}

	//	public double Total
	//	{
	//		get
	//		{
	//			return 0;
	//		}
	//	}

	//	public override string ToString()
	//	{
	//		return string.Format("Order: {0} items for ${1}",
	//			this.Products.Count, Total);
	//	}
	//}
}


