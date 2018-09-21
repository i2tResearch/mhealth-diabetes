using System;
using System.Globalization;

namespace BetoApp.Converters
{
    public class EqualToObjectConverter : BoolToObjectConverter
    {
        public EqualToObjectConverter(object boolValue, object falseValue) : base(boolValue, falseValue) { }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == parameter ? _trueValue : _falseValue;
        }
    }
}
