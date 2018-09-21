using System;
using System.Globalization;

namespace BetoApp.Converters
{
    public class EqualToStringConverter : BoolToStringConverter
    {
        public EqualToStringConverter(string boolValue, string falseValue) : base(boolValue, falseValue) { }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == parameter ? _trueValue : _falseValue;
        }
    }
}
