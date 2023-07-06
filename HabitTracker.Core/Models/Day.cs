using System.Collections.ObjectModel;

namespace HabitTracker.Core.Models;

public class Day : ObservableObject
{
    private ObservableCollection<HabitCompletion> _habits = new();
    private bool _isCompleted;
    public DateTime Date { get; set; }

    public ObservableCollection<HabitCompletion> Habits
    {
        get => _habits;
        set
        {
            if (Equals(value, _habits)) return;
            _habits = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsCompleted));
        }
    }

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
}