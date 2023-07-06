using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Core.Models;

public class HabitCompletion : ObservableObject
{
    [Key] public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }

    [ForeignKey("HabitId")] public int HabitId { get; set; }
    public Habit Habit { get; set; } = null!;
}