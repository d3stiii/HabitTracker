using System;
using HabitTracker.Core;

namespace HabitTracker.UI.ViewModels;

public class MainViewModel : ObservableObject
{
    private readonly HomeViewModel _homeViewModel;
    private readonly HabitsViewModel _habitsViewModel;
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

    public RelayCommand OpenHomeViewCommand => new(o => CurrentView = _homeViewModel);
    public RelayCommand OpenHabitsViewCommand => new(o => CurrentView = _habitsViewModel);

    public MainViewModel()
    {
        _homeViewModel = new HomeViewModel();
        _habitsViewModel = new HabitsViewModel();
        CurrentView = _homeViewModel;
    }
}