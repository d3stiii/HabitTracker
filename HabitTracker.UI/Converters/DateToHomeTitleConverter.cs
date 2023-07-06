using System;
using System.Globalization;
using System.Windows.Data;

namespace HabitTracker.UI.Converters;

public class DateToHomeTitleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = (DateTime)value;
        return (date - DateTime.Today).Days switch
        {
            0 => "Today",
            -1 => "Yesterday",
            1 => "Tomorrow",
            _ => date.Date.ToString("dd/MM/yyyy")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}