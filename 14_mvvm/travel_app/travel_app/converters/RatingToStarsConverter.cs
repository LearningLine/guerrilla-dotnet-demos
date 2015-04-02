using System;
using System.Globalization;
using System.Windows.Data;

namespace travel_app.converters
{
	class RatingToStarsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is int))
				return "";

			int rating = (int) value;
			return "".PadLeft(rating, '*');
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
