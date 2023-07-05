using System;
using System.Collections.ObjectModel;
using HabitTracker.Core;
using HabitTracker.Core.Models;
using HabitTracker.Core.Services;

namespace HabitTracker.UI.ViewModels;

public class AddHabitViewModel : ViewModel
{
    private readonly NavigationService _navigationService;
    private ObservableCollection<DayOfWeekModel> _daysOfWeek;

    public AddHabitViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        DaysOfWeek = new ObservableCollection<DayOfWeekModel>
        {
            new() { Day = DayOfWeek.Monday },
            new() { Day = DayOfWeek.Tuesday },
            new() { Day = DayOfWeek.Wednesday },
            new() { Day = DayOfWeek.Thursday },
            new() { Day = DayOfWeek.Friday },
            new() { Day = DayOfWeek.Saturday },
            new() { Day = DayOfWeek.Sunday },
        };
    }

    public ObservableCollection<DayOfWeekModel> DaysOfWeek
    {
        get => _daysOfWeek;
        set
        {
            if (Equals(value, _daysOfWeek)) return;
            _daysOfWeek = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public RelayCommand SelectDayCommand => new(o =>
    {
        var day = (DayOfWeekModel)o;
        day.IsChecked = !day.IsChecked;
    });

    public RelayCommand AddHabitCommand => new(o =>
    {
        //TODO: Add habit
        _navigationService.NavigateTo<HabitsViewModel>();
    });
}