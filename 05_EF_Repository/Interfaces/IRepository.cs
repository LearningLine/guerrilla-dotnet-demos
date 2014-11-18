using System.Linq;

namespace Interfaces
{
	public interface IRepository<T>
	{
		void Add(T genre);
		void Delete(T genre);

		IQueryable<T> Items { get; }		
	}
}