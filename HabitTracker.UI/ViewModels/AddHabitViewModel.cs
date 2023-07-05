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
            new() { Title = "MO" },
            new() { Title = "TU" },
            new() { Title = "WE" },
            new() { Title = "TH" },
            new() { Title = "FR" },
            new() { Title = "SA" },
            new() { Title = "SU" },
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