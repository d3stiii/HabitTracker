using HabitTracker.Core.Models;
using HabitTracker.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services;

public class HabitRepository
{
    private readonly DatabaseContext _database;

    public HabitRepository(DatabaseContext database)
    {
        _database = database;
    }

    public async Task<List<Habit>> GetAllHabits() =>
        await _database.Habits.ToListAsync();

    public async Task<Habit> GetHabitByName(string name) =>
        (await _database.Habits.FirstOrDefaultAsync(x => x.Title == name))!;

    public async Task AddHabit(Habit habit)
    {
        await _database.Habits.AddAsync(habit);
        await _database.SaveChangesAsync();
    }

    public async Task UpdateHabit(Habit habit)
    {
        _database.Habits.Update(habit);
        await _database.SaveChangesAsync();
    }

    public async Task DeleteHabit(Habit habit)
    {
        _database.Habits.Remove(habit);
        await _database.SaveChangesAsync();
    }
}