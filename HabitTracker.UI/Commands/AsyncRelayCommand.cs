using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HabitTracker.UI.Commands;

public class AsyncRelayCommand : ICommand
{
    private readonly Func<object, bool> _canExecute;
    private readonly Func<object, Task> _execute;
    private bool _isExecuting;

    public AsyncRelayCommand(Func<object, Task> execute, Func<object, bool> canExecute = null!)
    {
        _canExecute = canExecute;
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) =>
        !_isExecuting && (_canExecute == null! || _canExecute(parameter!));

    public async void Execute(object? parameter)
    {
        _isExecuting = true;
        await _execute(parameter!);
        _isExecuting = false;
    }
}