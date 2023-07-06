using System;
using System.Collections.ObjectModel;
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
    private readonly HabitRepository _habitRepository;
    private readonly CompletionsRepository _completionsRepository;
    private readonly HabitProcessor _habitProcessor;
    private readonly TrackingDaysManager _trackingDaysManager;
    private ObservableCollection<DateTime> _trackingDays = null!;
    private ObservableCollection<HabitCompletion> _habitsOnDay = null!;
    private string _selectedDayDate = null!;

    public HomeViewModel(HabitRepository habitRepository, CompletionsRepository completionsRepository,
        HabitProcessor habitProcessor, TrackingDaysManager trackingDaysManager)
    {
        _habitRepository = habitRepository;
        _completionsRepository = completionsRepository;
        _habitProcessor = habitProcessor;
        _trackingDaysManager = trackingDaysManager;
    }

    public ObservableCollection<DateTime> TrackingDays
    {
        get => _trackingDays;
        set
        {
            if (_trackingDays == value) return;
            _trackingDays = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public ObservableCollection<HabitCompletion> HabitsOnDay
    {
        get => _habitsOnDay;
        set
        {
            if (Equals(value, _habitsOnDay)) return;
            _habitsOnDay = value;
            OnPropertyChanged();
        }
    }

    public string SelectedDayDate
    {
        get => _selectedDayDate;
        set
        {
            if (value == _selectedDayDate) return;
            _selectedDayDate = value;
            OnPropertyChanged();
        }
    }

    public AsyncRelayCommand SelectDayCommand => new(async o =>
    {
        var date = (DateTime)o;

        SelectedDayDate = GetDate(date);
        HabitsOnDay = new ObservableCollection<HabitCompletion>(await _completionsRepository.GetHabitsForDate(date));
    });

    public AsyncRelayCommand CheckHabitCommand => new(async o =>
    {
        var completion = (HabitCompletion)o;
        completion.IsCompleted = !completion.IsCompleted;

        await _completionsRepository.Save();
    });

    public override async Task OnInitializeAsync()
    {
        TrackingDays = new ObservableCollection<DateTime>();
        var startDate = DateTime.Today.AddDays(-3);
        var allHabits = await _habitRepository.GetAllItems();

        for (var i = 0; i < 7; i++)
        {
            var date = startDate.AddDays(i);
            Application.Current.Dispatcher.Invoke(() => { _trackingDaysManager.AddTrackingDay(TrackingDays, date); });
            
            var habitsOnDay = _habitProcessor.GetHabitsOnDay(allHabits, date);

            if (habitsOnDay.Count == 0)
                continue;

            await _habitProcessor.ProcessHabitsOnDay(habitsOnDay, date);
        }
        
        await _completionsRepository.Save();
        SelectDayCommand.Execute(_trackingDaysManager.GetTodayCalendarItem(TrackingDays));
    }

    private static string GetDate(DateTime date) =>
        (date - DateTime.Today).Days switch
        {
            0 => "Today",
            -1 => "Yesterday",
            1 => "Tomorrow",
            _ => date.Date.ToString("dd/MM/yyyy")
        };
}