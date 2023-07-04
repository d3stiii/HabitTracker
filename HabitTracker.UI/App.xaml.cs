using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using HabitTracker.Core;
using HabitTracker.UI.ViewModels;
using HabitTracker.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HabitTracker.UI;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var culture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<HabitsViewModel>();
        services.AddSingleton<NavigationService>();
        services.AddSingleton<Func<Type, ViewModel>>(provider =>
            viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }
}