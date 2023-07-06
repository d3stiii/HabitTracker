namespace HabitTracker.Services.Repositories;

public abstract class Repository<T> where T : class
{
    public abstract Task<List<T>> GetAllItems();
    public abstract Task<T> GetItemById(int id);
    public abstract Task AddItem(T item);
    public abstract void DeleteItem(T item);
    public abstract Task Save();
}