namespace HabitTracker.Services;

public class TrackingDaysManager
{
    public void AddTrackingDay(ICollection<DateTime> calendarItems, DateTime date) => 
        calendarItems.Add(date);

    public DateTime GetTodayCalendarItem(ICollection<DateTime> calendarItems) => 
        calendarItems.First(x => x.Date == DateTime.Today);
}