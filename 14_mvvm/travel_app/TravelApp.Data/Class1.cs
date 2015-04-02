using System;
using System.IO;
using System.Linq;

namespace TravelApp.Data
{
	public class Db
	{
		public static Destination[] AllDestinations()
		{
			var rand = new Random();
			var images = GetAllImages();
			var id = 0;

			var destinations =
				(from i in images
					orderby i.ToLower()
					select new Destination
					{
						Id = ++id,
						Name = Path.GetFileNameWithoutExtension(i)
							.Replace("-", " ").Replace("_", " ").Trim(),
						Picture = "images/" + i,
						Rating = rand.Next(5, 11)
					}).ToArray();

			return destinations;
		}

		private static string[] GetAllImages()
		{
			return new[]
			{
				"Cape-Lookout-Tillamook-County-Oregon.jpg",
				"ColumbiaRiverGorge_20110602.jpg",
				"Eagle-Cap-Wilderness-Oregon.jpg",
				"elowah-falls-columbia-river-gorge.jpg",
				"fbea69788733bf2f14d13c0f0394c629.jpg",
				"foggy-day-at-bon-oregon-coast-247771.jpg",
				"forest_trail_in_mount_revelstoke_canada.jpg",
				"getimage.jpg",
				"getimage.php_.jpg",
				"Glacier Lake, Wallowa-Whitman National Forest, Eagle Cap Wilderness Area, Oregon.jpg",
				"hamilton.png",
				"Mount Hood and Fisherman on Trillium Lake, Oregon.jpg",
				"mountain_wilderness-wallpaper-1920x1440.jpg",
				"oregon-coast-3.jpg",
				"Oregon-Coast-danjacobs-10802757101.jpg",
				"oregon-desktop-wallpapers-for-background-full-free.jpg",
				"Ore_coast-Oregon-Wallpaper.jpg",
				"p741740566-6.jpg",
				"phpThumb.jpg",
				"Sparks_Lake_South_Sister_Peak_Deschutes_National_Forest._Oregon.jpg",
				"toketee_falls_oregon_wallpaper-normal.jpg"
			};
		}
	}
}