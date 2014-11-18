using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ef_data;

namespace Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
	    Genre FindById(int id);

	    Album[] FindAlbumsByGenre(int id);
    }
}
