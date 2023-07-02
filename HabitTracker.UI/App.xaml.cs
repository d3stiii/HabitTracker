using System.Globalization;
using System.Threading;
using System.Windows;

namespace HabitTracker.UI;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var culture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        base.OnStartup(e);
    }
}