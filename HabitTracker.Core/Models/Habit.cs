using System.Collections.ObjectModel;

namespace HabitTracker.Core.Models;

public class Habit
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ObservableCollection<DayOfWeek> DaysOfWeek { get; set; } = new();
}