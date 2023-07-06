using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HabitTracker.Core;
using HabitTracker.Core.Models;
using HabitTracker.Services;
using HabitTracker.Services.Repositories;
using HabitTracker.UI.Commands;

namespace HabitTracker.UI.ViewModels;

public class HabitsViewModel : ViewModel
{
    private readonly NavigationService _navigationService;
    private readonly HabitRepository _habitRepository;
    private ObservableCollection<Habit> _habits = null!;

    public HabitsViewModel(NavigationService navigationService, HabitRepository habitRepository)
    {
        _navigationService = navigationService;
        _habitRepository = habitRepository;
    }

    public RelayCommand OpenAddHabitViewCommand => new(_ => _navigationService.NavigateTo<HabitSettingsViewModel>());
    public AsyncRelayCommand DeleteHabitCommand => new(DeleteHabit);

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

    public override async Task OnInitializeAsync() =>
        await LoadHabits();

    private async Task LoadHabits() =>
        Habits = new ObservableCollection<Habit>(await _habitRepository.GetAllItems());

    private async Task DeleteHabit(object obj)
    {
        var habit = (Habit)obj;
        _habitRepository.DeleteItem(habit);
        await _habitRepository.Save();
        await LoadHabits();
    }
}