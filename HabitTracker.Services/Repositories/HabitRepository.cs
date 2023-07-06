using HabitTracker.Core.Models;
using HabitTracker.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services.Repositories;

public class HabitRepository : Repository<Habit>
{
    private readonly DatabaseContext _database;
    
    public HabitRepository(DatabaseContext database)
    {
        _database = database;
    }

    public override async Task<List<Habit>> GetAllItems() =>
        await _database.Habits.Include(x => x.Completions).ToListAsync();

    public override async Task<Habit> GetItemById(int id) =>
        (await _database.Habits.FirstOrDefaultAsync(x => x.Id == id))!;

    public override async Task AddItem(Habit item) => 
        await _database.Habits.AddAsync(item);

    public override void DeleteItem(Habit item) => 
        _database.Habits.Remove(item);

    public override async Task Save() => 
        await _database.SaveChangesAsync();
}