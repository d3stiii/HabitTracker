using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HabitTracker.Core;
using HabitTracker.Core.Models;
using HabitTracker.Services;
using HabitTracker.Services.Repositories;
using HabitTracker.UI.Commands;

namespace HabitTracker.UI.ViewModels;

public sealed class HomeViewModel : ViewModel
{
    private const int TrackingDaysCount = 7;
    private readonly HabitRepository _habitRepository;
    private readonly CompletionsRepository _completionsRepository;
    private readonly HabitProcessor _habitProcessor;
    private ObservableCollection<Day> _days = null!;
    private Day _selectedDay = null!;

    public HomeViewModel(HabitRepository habitRepository, CompletionsRepository completionsRepository,
        HabitProcessor habitProcessor)
    {
        _habitRepository = habitRepository;
        _completionsRepository = completionsRepository;
        _habitProcessor = habitProcessor;
    }

    public ObservableCollection<Day> Days
    {
        get => _days;
        set
        {
            if (Equals(value, _days)) return;
            _days = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public Day SelectedDay
    {
        get => _selectedDay;
        set
        {
            if (Equals(value, _selectedDay)) return;
            _selectedDay = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public RelayCommand SelectDayCommand => new(o => SelectedDay = (Day)o);

    public AsyncRelayCommand CheckHabitCommand => new(async o =>
    {
        var completion = (HabitCompletion)o;
        completion.IsCompleted = !completion.IsCompleted;
        await _completionsRepository.Save();
        SelectedDay.IsCompleted = SelectedDay.Habits.All(x => x.IsCompleted);
    });

    public override async Task OnInitializeAsync()
    {
        var startDate = DateTime.Today.AddDays(-3);
        var allHabits = await _habitRepository.GetAllItems();
        InitializeDays(startDate);
        await ProcessHabits(startDate, allHabits);
        await _completionsRepository.Save();

        SelectToday();
    }

    private void InitializeDays(DateTime startDate)
    {
        Days = new ObservableCollection<Day>();

        for (var i = 0; i < TrackingDaysCount; i++)
        {
            var date = startDate.AddDays(i);
            var day = new Day
            {
                Date = date,
            };
            Application.Current.Dispatcher.Invoke(() => { Days.Add(day); });
        }
    }

    private async Task ProcessHabits(DateTime startDate, List<Habit> allHabits)
    {
        for (var i = 0; i < TrackingDaysCount; i++)
        {
            var date = startDate.AddDays(i);
            var day = Days[i];
            var habitsOnDay = _habitProcessor.GetHabitsOnDay(allHabits, date);

            if (habitsOnDay.Count == 0)
            {
                day.IsCompleted = true;
                continue;
            }

            var completions = await _habitProcessor.AddMissingHabitCompletions(habitsOnDay, date);

            day.Habits = new ObservableCollection<HabitCompletion>(completions);
            day.IsCompleted = completions.All(x => x.IsCompleted);
        }
    }

    private void SelectToday() => SelectDayCommand.Execute(Days.FirstOrDefault(x => x.Date == DateTime.Today));
}