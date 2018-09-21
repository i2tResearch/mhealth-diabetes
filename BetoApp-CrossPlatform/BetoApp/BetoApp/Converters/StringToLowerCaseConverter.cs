using System;
using System.Globalization;
using Xamarin.Forms;

namespace BetoApp.Converters
{
    public class StringToLowerCaseConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            return str != null ? str.ToLower() : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
