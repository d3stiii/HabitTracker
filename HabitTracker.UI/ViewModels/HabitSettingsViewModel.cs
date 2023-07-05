using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HabitTracker.Core;
using HabitTracker.Core.Models;
using HabitTracker.Services;
using HabitTracker.UI.Commands;

namespace HabitTracker.UI.ViewModels;

public class HabitSettingsViewModel : ViewModel
{
    private readonly NavigationService _navigationService;
    private readonly HabitRepository _habitRepository;
    private ObservableCollection<DayOfWeekModel> _daysOfWeek = null!;
    private string _habitTitle = null!;
    private string _habitDescription = null!;

    public HabitSettingsViewModel(NavigationService navigationService, HabitRepository habitRepository)
    {
        _navigationService = navigationService;
        _habitRepository = habitRepository;
    }

    public string HabitDescription
    {
        get => _habitDescription;
        set
        {
            if (value == _habitDescription) return;
            _habitDescription = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public string HabitTitle
    {
        get => _habitTitle;
        set
        {
            if (value == _habitTitle) return;
            _habitTitle = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
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

    public AsyncRelayCommand AddHabitCommand => new(AddHabit, CanAdd);

    public override void OnInitialize()
    {
        ResetInputs();
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

    private bool CanAdd(object obj) =>
        !string.IsNullOrEmpty(HabitTitle) && !string.IsNullOrEmpty(HabitDescription) &&
        DaysOfWeek.Any(x => x.IsChecked);

    private async Task AddHabit(object obj)
    {
        await _habitRepository.AddHabit(new Habit
        {
            DaysOfWeek = new ObservableCollection<DayOfWeek>(_daysOfWeek.Where(x => x.IsChecked)
                .Select(x => x.Day)),
            Description = HabitDescription,
            Title = HabitTitle
        });

        _navigationService.NavigateTo<HabitsViewModel>();
    }

    private void ResetInputs()
    {
        HabitTitle = string.Empty;
        HabitDescription = string.Empty;
    }
}