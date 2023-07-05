using HabitTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.DataLayer;

public sealed class DatabaseContext : DbContext
{
    public DbSet<Habit> Habits { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}