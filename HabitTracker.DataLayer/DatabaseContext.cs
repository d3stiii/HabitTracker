using HabitTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.DataLayer;

public sealed class DatabaseContext : DbContext
{
    public DbSet<Habit> Habits { get; set; } = null!;
    public DbSet<HabitCompletion> HabitCompletions { get; set; } = null!;

    public DatabaseContext() =>
        Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=habittracker.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<HabitCompletion>()
            .HasOne(hc => hc.Habit)
            .WithMany(h => h.Completions)
            .HasForeignKey(hc => hc.HabitId);
}