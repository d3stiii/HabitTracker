namespace HabitTracker.Core;

public class NavigationService : ObservableObject
{
    private readonly Func<Type, ViewModel> _viewModelFactory;
    private ViewModel _currentView;

    public NavigationService(Func<Type, ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public ViewModel CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public void NavigateTo<TViewModel>() where TViewModel : ObservableObject
    {
        var viewModel = _viewModelFactory(typeof(TViewModel));
        CurrentView = viewModel;
    }
}