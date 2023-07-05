using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Core.Models;

public class Habit
{
    [Key] public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ObservableCollection<DayOfWeek> DaysOfWeek { get; set; } = new();
}