using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Core.Models;

public class HabitCompletion : ObservableObject
{
    private bool _isCompleted;
    [Key] public int Id { get; set; }
    public DateTime Date { get; set; }

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            if (value == _isCompleted) return;
            _isCompleted = value;
            OnPropertyChanged();
        }
    }

    [ForeignKey("HabitId")] public int HabitId { get; set; }
    public Habit Habit { get; set; } = null!;
}