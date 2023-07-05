using System;
using System.Collections.ObjectModel;
using HabitTracker.Core;
using HabitTracker.Core.Models;
using HabitTracker.Core.Services;

namespace HabitTracker.UI.ViewModels;

public class HabitsViewModel : ViewModel
{
    private readonly NavigationService _navigationService;
    private ObservableCollection<Habit> _habits;

    public HabitsViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        //TODO: Load habits
        Habits = new ObservableCollection<Habit>
        {
            new()
            {
                Title = "Code",
                DaysOfWeek = new ObservableCollection<DayOfWeek>
                {
                    DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Sunday, DayOfWeek.Saturday,
                    DayOfWeek.Tuesday
                }
            },
            new()
            {
                Title = "Read",
                DaysOfWeek = new ObservableCollection<DayOfWeek>
                {
                    DayOfWeek.Saturday, DayOfWeek.Tuesday
                }
            },
            new()
            {
                Title = "Watch films",
                DaysOfWeek = new ObservableCollection<DayOfWeek>
                {
                    DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Saturday,
                    DayOfWeek.Sunday, DayOfWeek.Thursday
                }
            }
        };
    }

    public RelayCommand OpenAddHabitViewCommand => new(o => _navigationService.NavigateTo<AddHabitViewModel>());
    
    public ObservableCollection<Habit> Habits
    {
        get => _habits;
        set
        {
            if (Equals(value, _habits)) return;
            _habits = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }
}