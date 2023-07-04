namespace HabitTracker.Core.Models;

public class CalendarItem
{
    public DateTime Date { get; set; }
    public bool IsComplete { get; set; }

    public override string ToString() => 
        $"{Date:dddd}";
}