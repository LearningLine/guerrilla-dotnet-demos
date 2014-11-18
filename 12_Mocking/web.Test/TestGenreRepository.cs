using System;
using System.Collections.Generic;
using System.Linq;
using ef_data;
using Interfaces;

namespace web.Test
{
	internal class TestGenreRepository : IGenreRepository
	{
		public void Add(Genre genre)
		{
			throw new NotImplementedException();
		}

		public void Delete(Genre genre)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Genre> Items
		{
			get
			{
				return new List<Genre>
				{
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
					new Genre {Name = "Test"},
				}.AsQueryable();
			}
		}

		public Genre FindById(int id)
		{
			return new Genre();
		}


		public Album[] FindAlbumsByGenre(int id)
		{
			throw new NotImplementedException();
		}
	}
}