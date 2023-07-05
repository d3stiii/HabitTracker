namespace HabitTracker.Core;

public abstract class ViewModel : ObservableObject
{
    public virtual void OnInitialize() { }

    public virtual Task OnInitializeAsync() => Task.CompletedTask;
}