using System;
using System.Globalization;
using MvvmCross.Converters;

namespace NewsAppNative.Droid.Converters
{
    public class InvertBoolValueConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }

    public class DateTimeToStringValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString("dddd, dd MMMM yyyy");
        }
    }
}
