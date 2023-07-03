using System;
using System.Collections.ObjectModel;
using System.Linq;
using HabitTracker.Core;
using HabitTracker.Core.Models;

namespace HabitTracker.UI.ViewModels;

public sealed class HomeViewModel : ObservableObject
{
    private ObservableCollection<CalendarItem> _calendarItems;
    private string _selectedDayDate;

    public ObservableCollection<CalendarItem> CalendarItems
    {
        get => _calendarItems;
        set
        {
            if (_calendarItems == value) return;
            _calendarItems = value ?? throw new ArgumentNullException(nameof(value));
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

    public RelayCommand SelectDayCommand => new(o =>
    {
        var calendarItem = (CalendarItem)o;

        SelectedDayDate = GetDate(calendarItem.Date);
    });

    public HomeViewModel()
    {
        CalendarItems = new ObservableCollection<CalendarItem>();
        var startDate = DateTime.Today.AddDays(-3);

        for (var i = 0; i < 7; i++) //TODO: load days data and set completed flag
        {
            CalendarItems.Add(new CalendarItem
            {
                Date = startDate.AddDays(i)
            });
        }

        SelectDayCommand.Execute(CalendarItems.First(x => x.Date == DateTime.Today));
    }

    private static string GetDate(DateTime date)
    {
        return (date.Date - DateTime.Today).Days switch
        {
            0 => "Today",
            -1 => "Yesterday",
            1 => "Tomorrow",
            _ => date.Date.ToString("dd/MM/yyyy")
        };
    }
}