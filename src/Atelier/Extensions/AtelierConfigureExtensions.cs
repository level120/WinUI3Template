using Atelier.ViewModels;

using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Windows.AppNotifications;

namespace Atelier.Extensions;

/// <summary>
/// Internal extension methods for configuring the Atelier application.
/// </summary>
internal static class AtelierConfigureExtensions
{
    /// <summary>
    /// Configures the services for the Atelier application.
    /// </summary>
    /// <param name="ioc">ioc</param>
    public static void ConfigureServices(this Ioc ioc)
    {
        // Register the ViewModels and other services here.
        var serviceCollection = new ServiceCollection();

        // ViewModels
        serviceCollection.AddSingleton<ShellViewModel>();
        serviceCollection.AddSingleton<HomeViewModel>();
        serviceCollection.AddSingleton<SettingsViewModel>();
        serviceCollection.AddSingleton<DecryptLogViewModel>();

        // Services
        serviceCollection.AddSingleton(AppNotificationManager.Default);

        ioc.ConfigureServices(serviceCollection.BuildServiceProvider());
    }
}