using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace BingeBuddy
{
    public class StarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rating && parameter is string starStr && int.TryParse(starStr, out int star))
            {
                return rating >= star ? Colors.Gold : Colors.Gray;
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
