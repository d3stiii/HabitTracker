using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HabitTracker.Core;
using HabitTracker.Core.Models;
using HabitTracker.Services;
using HabitTracker.Services.Repositories;
using HabitTracker.UI.Commands;

namespace HabitTracker.UI.ViewModels;

public class HabitSettingsViewModel : ViewModel
{
    private readonly NavigationService _navigationService;
    private readonly HabitRepository _habitRepository;
    private ObservableCollection<DayOfWeek> _displayHabitDaysOfWeek = null!;
    private ObservableCollection<DayOfWeek> _habitDaysOfWeek = null!;
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

    public ObservableCollection<DayOfWeek> DisplayDaysOfWeek
    {
        get => _displayHabitDaysOfWeek;
        set
        {
            if (Equals(value, _displayHabitDaysOfWeek)) return;
            _displayHabitDaysOfWeek = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DayOfWeek> HabitDaysOfWeek
    {
        get => _habitDaysOfWeek;
        set
        {
            if (Equals(value, _habitDaysOfWeek)) return;
            _habitDaysOfWeek = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public AsyncRelayCommand AddHabitCommand => new(AddHabit, CanAdd);

    public RelayCommand CheckDayCommand => new(o =>
    {
        var dayOfWeek = (DayOfWeek)o;
        if (HabitDaysOfWeek.Remove(dayOfWeek))
        {
            return;
        }

        HabitDaysOfWeek.Add(dayOfWeek);
    });

    public override void OnInitialize()
    {
        HabitDaysOfWeek = new ObservableCollection<DayOfWeek>();
        ResetInputs();
        InitializeDisplayDaysOfWeek();
    }

    private bool CanAdd(object obj) =>
        !string.IsNullOrWhiteSpace(HabitTitle) && !string.IsNullOrWhiteSpace(HabitDescription) && HabitDaysOfWeek.Any();

    private async Task AddHabit(object obj)
    {
        var habit = new Habit
        {
            DaysOfWeek = HabitDaysOfWeek,
            Description = HabitDescription,
            Title = HabitTitle
        };

        await _habitRepository.AddItem(habit);
        await _habitRepository.Save();

        _navigationService.NavigateTo<HabitsViewModel>();
    }

    private void ResetInputs()
    {
        HabitTitle = string.Empty;
        HabitDescription = string.Empty;
    }

    private void InitializeDisplayDaysOfWeek()
    {
        DisplayDaysOfWeek = new ObservableCollection<DayOfWeek>
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };
    }
}