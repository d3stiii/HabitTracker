using System;
using System.Globalization;
using System.Windows.Data;

namespace HabitTracker.UI.Converters;

public class TodayDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
        ((DateTime)value).Date == DateTime.Today;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
        throw new NotSupportedException();
}