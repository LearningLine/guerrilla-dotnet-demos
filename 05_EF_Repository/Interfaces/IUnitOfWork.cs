using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenreRepository Genres { get; }

		void Commit();
	}
}
