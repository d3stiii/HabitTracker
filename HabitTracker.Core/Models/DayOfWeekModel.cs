namespace HabitTracker.Core.Models;

public class DayOfWeekModel
{
    public DayOfWeek Day { get; set; }
    public bool IsChecked { get; set; }
    public string Title => Day.ToString()[..2].ToUpper();
}