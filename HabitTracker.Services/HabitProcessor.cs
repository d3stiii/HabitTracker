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

    public async Task<ICollection<HabitCompletion>> AddMissingHabitCompletions(IEnumerable<Habit> habitsOnDay,
        DateTime date)
    {
        var completions = new List<HabitCompletion>();
        foreach (var habit in habitsOnDay)
        {
            var completion = habit.Completions.FirstOrDefault(x => x.Date == date);
            if (completion != null)
            {
                completions.Add(completion);
                continue;
            }

            completion = new HabitCompletion
            {
                Habit = habit,
                Date = date,
                HabitId = habit.Id
            };

            await _completionsRepository.AddItem(completion);
            completions.Add(completion);
        }

        return completions;
    }
}