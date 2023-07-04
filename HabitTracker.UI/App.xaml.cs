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
        SetCulture("en-US");

        IServiceCollection services = new ServiceCollection();
        BindServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private static void BindServices(IServiceCollection services)
    {
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
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    private static void SetCulture(string cultureName)
    {
        var culture = new CultureInfo(cultureName);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}