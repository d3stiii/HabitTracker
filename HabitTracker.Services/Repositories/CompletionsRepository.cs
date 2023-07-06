using HabitTracker.Core.Models;
using HabitTracker.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services.Repositories;

public class CompletionsRepository : Repository<HabitCompletion>
{
    private readonly DatabaseContext _database;

    public CompletionsRepository(DatabaseContext database)
    {
        _database = database;
    }

    public async Task<IEnumerable<HabitCompletion>> GetHabitsForDate(DateTime date)
    {
        var allCompletions = await GetAllItems();
        var habitsOnDay = allCompletions.Where(x => x.Date == date);
        return habitsOnDay;
    }

    public override async Task<List<HabitCompletion>> GetAllItems() =>
        await _database.HabitCompletions.Include(x => x.Habit).ToListAsync();

    public override async Task<HabitCompletion> GetItemById(int id) =>
        (await _database.HabitCompletions.FirstOrDefaultAsync(x => x.Id == id))!;

    public override async Task AddItem(HabitCompletion item) =>
        await _database.HabitCompletions.AddAsync(item);

    public override void DeleteItem(HabitCompletion item) =>
        _database.Remove(item);

    public override async Task Save() =>
        await _database.SaveChangesAsync();
}