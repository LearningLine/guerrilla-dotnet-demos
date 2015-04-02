using System.Diagnostics;

namespace TravelApp.Data
{
	[DebuggerDisplay("Id: {Id}, Rating: {Rating} Name: {Name}")]
	public class Destination : BaseModel
	{
		
		private int id;
		private string name;
		private string picture;
		private int rating;

		public int Id
		{
			get { return id; }
			set { id = value; FirePropertyChanged(); }
		}

		public string Name
		{
			get { return name; }
			set { name = value; FirePropertyChanged(); }
		}

		public string Picture
		{
			get { return picture; }
			set { picture = value; FirePropertyChanged(); }
		}

		public int Rating
		{
			get { return rating; }
			set { rating = value; FirePropertyChanged(); }
		}

		public override string ToString()
		{
			return Name;
		}
	}
}