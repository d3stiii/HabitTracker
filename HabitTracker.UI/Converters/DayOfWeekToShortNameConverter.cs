using System;
using System.Globalization;
using System.Windows.Data;

namespace HabitTracker.UI.Converters;

public class DayOfWeekToShortNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var dayOfWeek = (DayOfWeek)value;
        return dayOfWeek.ToString()[..2].ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
        throw new NotSupportedException();
}