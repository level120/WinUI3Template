using Atelier.ViewModels;

using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

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
        var serviceCollection = new ServiceCollection();

        // Register the ViewModels and other services here.
        // Cannot reflection because if the way the project is AOT.
        serviceCollection.AddTransient<ShellViewModel>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        ioc.ConfigureServices(serviceProvider);
    }
}