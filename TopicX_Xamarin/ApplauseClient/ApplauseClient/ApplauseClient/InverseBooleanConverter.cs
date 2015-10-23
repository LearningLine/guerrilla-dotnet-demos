using System;
using System.Globalization;
using Xamarin.Forms;

namespace ApplauseClient
{
	public class InverseBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool output;
			var parsed = bool.TryParse(value.ToString(), out output);

			return !output;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}