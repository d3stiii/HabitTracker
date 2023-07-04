using HabitTracker.Core;

namespace HabitTracker.UI.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly NavigationService _navigationService;

    public MainViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.NavigateTo<HomeViewModel>();
    }

    public NavigationService NavigationService => _navigationService;

    public RelayCommand OpenHomeViewCommand => new(o => _navigationService.NavigateTo<HomeViewModel>());

    public RelayCommand OpenHabitsViewCommand => new(o => _navigationService.NavigateTo<HabitsViewModel>());
}