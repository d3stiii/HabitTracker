using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HabitTracker.UI.Converters;

public class DayOfWeekCollectionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var daysOfWeek = (value as ObservableCollection<DayOfWeek>)!
            .OrderBy(x => ((int)x + 6) % 7)
            .ToList();

        if (daysOfWeek is { Count: 7 }) return "Every day";

        var abbreviatedDays = daysOfWeek.Select(d => d.ToString()[..3]);
        return string.Join(", ", abbreviatedDays);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
        throw new NotSupportedException();
}