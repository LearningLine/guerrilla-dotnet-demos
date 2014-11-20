using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _99_MongoDB
{
	public class DataContext : MongoDB.Kennedy.ConcurrentDataContext
	{
		public DataContext(string databaseName = "GnetBooks", string serverName = "localhost") :
			base(databaseName, serverName)
		{
		}

		public IQueryable<Book> Books { get { return base.GetCollection<Book>(); } }
	}
}
