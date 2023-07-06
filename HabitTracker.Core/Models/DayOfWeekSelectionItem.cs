namespace HabitTracker.Core.Models;

public class DayOfWeekSelectionItem : ObservableObject
{
    public DayOfWeek Day { get; set; }
    public bool IsChecked { get; set; }
    public string Title => Day.ToString()[..2].ToUpper();
}