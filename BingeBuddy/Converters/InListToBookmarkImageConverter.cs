using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace BingeBuddy.Converters
{
    public class InListToBookmarkImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (value is bool inList && inList) ? "bookmark_inlist.png" : "bookmark.png";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
