using Interfaces;

namespace web.Test
{
	internal class TestUnitOfWork : IUnitOfWork
	{
		public IGenreRepository Genres
		{
			get { return new TestGenreRepository(); }
		}

		public void Commit()
		{
		}

		public void Dispose()
		{
		}
	}
}