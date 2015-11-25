using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace SimpleBinding
{
    public class BrushRange
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public Brush Brush { get; set; }
    }
    public class AgeToBrushConverter : IValueConverter
    {
        public List<BrushRange> BrushRanges { get; set; }

        public AgeToBrushConverter()
        {
            BrushRanges = new List<BrushRange>();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int age = (int) value;

            return BrushRanges
                .Where(r => r.Min <= age && r.Max >= age)
                .DefaultIfEmpty(new BrushRange() {Brush = Brushes.Ivory})
                .Select(r => r.Brush)
                .First();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}