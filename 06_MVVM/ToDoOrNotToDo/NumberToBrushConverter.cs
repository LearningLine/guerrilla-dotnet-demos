using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace ToDoOrNotToDo
{
    public class NumberBrush
    {
        public int Number { get; set; }
        public Brush Brush { get; set; }
    }
    public class NumberToBrushConverter : IValueConverter
    {
        public NumberToBrushConverter()
        {
            BrushMap = new List<NumberBrush>();
        }
        public List<NumberBrush> BrushMap { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int number = (int) value;

            return BrushMap.Where(bm => bm.Number == number)
                            .DefaultIfEmpty(new NumberBrush(){Brush = Brushes.Black})
                            .First().Brush;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}