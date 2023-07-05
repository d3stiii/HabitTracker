using HabitTracker.Core;

namespace HabitTracker.Services;

public class NavigationService : ObservableObject
{
    private readonly Func<Type, ViewModel> _viewModelFactory;
    private ViewModel _currentView = null!;

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

    public void NavigateTo<TViewModel>() where TViewModel : ViewModel
    {
        var viewModel = _viewModelFactory(typeof(TViewModel));
        
        viewModel.OnInitialize();
        Task.Run(async () => await viewModel.OnInitializeAsync());
        
        CurrentView = viewModel;
    }
}