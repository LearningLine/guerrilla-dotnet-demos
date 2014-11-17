using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Transactions;

namespace EFStarter
{
	class Program
	{
		static void Main(string[] args)
		{
			ChinookEntities ctx = new ChinookEntities()
			Artist artist = new Artist()
			{
				Name = "Rocky Mike"
			};

			//MusicType musicType = ctx.MusicTypes.First();

			//Album MikesAlbum = new Album()
			//{
			//	Artist = artist,
			//	Title = "Mikes Hip Hop Folk Rock Music Album",
			//	Songs =
			//	{
			//		new Track() { MediaTypeId=1, GenreId=1, Name = "Good Morning Molly"},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = "The Song that has no name"},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = "The song that could have a name"},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = "A name is ooptional"},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = "Something"},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = "Track B"},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = "I don't care"},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = ""},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = ""},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = ""},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = ""},
			//		new Track() { MediaTypeId=1, GenreId=1, Name = ""},

			//	}
			//};

			//ctx.Albums.Add(MikesAlbum);

			using (TransactionScope scope = new TransactionScope())
			{
				ctx.SaveChanges();


				scope.Complete();
			}




			var coolAlbums = ctx.CoolMuisc();

			foreach (Album album in coolAlbums)
			{
				Console.WriteLine(album.Title);
			}


			string title = "Rock";

			var query = from album in ctx.Albums
				//where album.Title.Contains("love")
				where album.Songs.Count() > 10 && album.Title.Contains(title)
				select album;



			// Extension method that takes a lambda
			// as opposed to using strings, keeps safe after refactoring

			foreach (var album in query.Include(a => a.Songs)) //.Include("Tracks"))
			{
				Console.WriteLine(album.Title);
				foreach (Track track in album.Songs)
				{
					Console.WriteLine("\t {0}",track.Name);
				}
			}
		}
	}
}









