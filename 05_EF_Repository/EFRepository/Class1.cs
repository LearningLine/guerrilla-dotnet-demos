using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ef_data;
using Interfaces;

namespace EFRepository
{
    public class EFGenreRepository : IGenreRepository
    {
	    private ChinookEntities ctx;

	    public EFGenreRepository(ChinookEntities ctx)
	    {
		    this.ctx = ctx;
	    }

	    public void Add(Genre genre)
		{
			ctx.Genres.Add(genre);

			ctx.SaveChanges();
		}

		public void Delete(Genre genre)
		{
			ctx.Genres.Remove(genre);
		}

		public IQueryable<Genre> Items
		{
			get { return ctx.Genres; }
		}

		public Genre FindById(int id)
		{
			return Items.Single(g => g.GenreId == id);
		}


		public Album[] FindAlbumsByGenre(int id)
		{
			return (from a in ctx.Albums.Include("Artist")
					where a.Tracks.Any(t => t.GenreId == id)
					orderby a.Title
					select a).ToArray();
		}
	}

	public class EfUnitOfWork : IUnitOfWork
	{
		readonly ChinookEntities ctx = new ChinookEntities();
		private readonly IGenreRepository genres;

		public EfUnitOfWork()
		{
			genres = new EFGenreRepository(ctx);
		}
		public IGenreRepository Genres
		{
			get { return genres; }
		}

		public void Commit()
		{
			ctx.SaveChanges();
		}

		public void Dispose()
		{
			ctx.Dispose();
		}
	}

}
