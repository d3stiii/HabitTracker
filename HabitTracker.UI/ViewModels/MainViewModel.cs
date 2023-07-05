using HabitTracker.Core;
using HabitTracker.Services;
using HabitTracker.UI.Commands;

namespace HabitTracker.UI.ViewModels;

public class MainViewModel : ViewModel
{
    public MainViewModel(NavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.NavigateTo<HomeViewModel>();
    }

    public NavigationService NavigationService { get; }

    public RelayCommand OpenHomeViewCommand => new(_ => NavigationService.NavigateTo<HomeViewModel>());

    public RelayCommand OpenHabitsViewCommand => new(_ => NavigationService.NavigateTo<HabitsViewModel>());
}