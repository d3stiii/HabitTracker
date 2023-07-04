using HabitTracker.Core;

namespace HabitTracker.UI.ViewModels;

public class MainViewModel : ViewModel
{
    public MainViewModel(NavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.NavigateTo<HomeViewModel>();
    }

    public NavigationService NavigationService { get; }

    public RelayCommand OpenHomeViewCommand => new(o => NavigationService.NavigateTo<HomeViewModel>());

    public RelayCommand OpenHabitsViewCommand => new(o => NavigationService.NavigateTo<HabitsViewModel>());
}