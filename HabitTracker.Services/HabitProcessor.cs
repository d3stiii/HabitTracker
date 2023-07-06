using HabitTracker.Core.Models;
using HabitTracker.Services.Repositories;

namespace HabitTracker.Services;

public class HabitProcessor
{
    private readonly CompletionsRepository _completionsRepository;

    public HabitProcessor(CompletionsRepository completionsRepository)
    {
        _completionsRepository = completionsRepository;
    }

    public List<Habit> GetHabitsOnDay(List<Habit> allHabits, DateTime date) =>
        allHabits.Where(x => x.DaysOfWeek.Contains(date.DayOfWeek)).ToList();

    public async Task ProcessHabitsOnDay(List<Habit> habitsOnDay, DateTime date)
    {
        foreach (var habit in habitsOnDay)
        {
            var completion = habit.Completions.FirstOrDefault(x => x.Date == date);
            if (completion != null) continue;
            completion = new HabitCompletion
            {
                Habit = habit,
                Date = date,
                HabitId = habit.Id
            };

            await _completionsRepository.AddItem(completion);
        }
    }
}