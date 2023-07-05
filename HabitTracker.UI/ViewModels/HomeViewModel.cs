using System;
using System.Collections.ObjectModel;
using System.Linq;
using HabitTracker.Core;
using HabitTracker.Core.Models;
using HabitTracker.UI.Commands;

namespace HabitTracker.UI.ViewModels;

public sealed class HomeViewModel : ViewModel
{
    private ObservableCollection<CalendarItem> _calendarItems = null!;
    private string _selectedDayDate = null!;

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

    private static string GetDate(DateTime date) =>
        (date.Date - DateTime.Today).Days switch
        {
            0 => "Today",
            -1 => "Yesterday",
            1 => "Tomorrow",
            _ => date.Date.ToString("dd/MM/yyyy")
        };

    public override void OnInitialize()
    {
        CalendarItems = new ObservableCollection<CalendarItem>();
        var startDate = DateTime.Today.AddDays(-3);

        for (var i = 0; i < 7; i++)
        {
            //TODO: load days data and set completed flag
            CalendarItems.Add(new CalendarItem
            {
                Date = startDate.AddDays(i)
            });
        }

        SelectDayCommand.Execute(CalendarItems.First(x => x.Date == DateTime.Today));
    }
}