using System;
using HabitTracker.Core;

namespace HabitTracker.UI.ViewModels;

public class MainViewModel : ObservableObject
{
    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            if (Equals(value, _currentView)) return;
            _currentView = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public RelayCommand OpenHomeViewCommand => new(o => CurrentView = new HomeViewModel());
    public RelayCommand OpenHabitsViewCommand => new(o => CurrentView = new HabitsViewModel());

    public MainViewModel()
    {
        CurrentView = new HabitsViewModel();
    }
}