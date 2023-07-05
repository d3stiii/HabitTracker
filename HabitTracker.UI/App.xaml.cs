using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using HabitTracker.Core;
using HabitTracker.DataLayer;
using HabitTracker.Services;
using HabitTracker.UI.ViewModels;
using HabitTracker.UI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HabitTracker.UI;

public partial class App
{
    private ServiceProvider _serviceProvider = null!;

    public App()
    {
        SetCulture("en-US");
        BindServices();
    }

    private void BindServices()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddDbContext<DatabaseContext>(options =>
                options.UseSqlite("Data Source=habittracker.db"))
            .AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            })
            .AddSingleton<MainViewModel>()
            .AddSingleton<HomeViewModel>()
            .AddSingleton<HabitsViewModel>()
            .AddSingleton<HabitSettingsViewModel>()
            .AddSingleton<NavigationService>()
            .AddSingleton<DatabaseContext>()
            .AddSingleton<HabitRepository>()
            .AddSingleton<Func<Type, ViewModel>>(provider =>
                viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();
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