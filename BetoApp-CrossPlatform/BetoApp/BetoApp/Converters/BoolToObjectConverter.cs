using System;
using System.Globalization;
using Xamarin.Forms;

namespace BetoApp.Converters
{
    public class BoolToObjectConverter : IValueConverter
    {
        protected object _trueValue;
        protected object _falseValue;

        public BoolToObjectConverter(object trueValue, object falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? _trueValue : _falseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
