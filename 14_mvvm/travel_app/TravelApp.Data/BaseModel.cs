using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TravelApp.Data
{
	public class BaseModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		protected void FirePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}