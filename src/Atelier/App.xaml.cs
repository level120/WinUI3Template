using System.Diagnostics;

using Atelier.Extensions;
using Atelier.Helpers;
using Atelier.Views;

using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Atelier;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        UnhandledException += OnUnhandledException;

        InitializeComponent();
    }

    /// <summary>
    /// Application's main window.
    /// </summary>
    public static Window? MainWindow { get; private set; }

    /// <summary>
    /// Gets the frame of the StartupWindow.
    /// </summary>
    /// <returns>The frame of the StartupWindow.</returns>
    public static Frame? GetRootFrame()
    {
        if (MainWindow?.Content is Shell rootPage)
        {
            return rootPage.FindName("RootFrame") as Frame;
        }

        return null;
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Ioc.Default.ConfigureServices();
        AppNotificationManager.Default.Register();

        MainWindow = WindowHelper.CreateWindow();
        MainWindow.Title = "Atelier";
        MainWindow.AppWindow.SetIcon("Assets/app.ico");
        MainWindow.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        MainWindow.AppWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;

#if DEBUG
        if (Debugger.IsAttached)
        {
            DebugSettings.BindingFailed += OnBindingFailed;
        }
#endif
        // Do not repeat app initialization when the Window already has content,
        // just ensure that the window is active
        if (MainWindow.Content is not Frame)
        {
            // Create a Frame to act as the navigation context and navigate to the first page
            MainWindow.Content = new Frame();
        }

        if (MainWindow.Content is Frame rootFrame)
        {
            rootFrame.NavigationFailed -= OnNavigationFailed;
            rootFrame.NavigationFailed += OnNavigationFailed;
            rootFrame.Navigate(typeof(Shell), args.Arguments);
        }

        MainWindow.Activate();
    }

    private static void OnBindingFailed(object sender, BindingFailedEventArgs e)
    {
        // Ignore the exception from NonExistentProperty in BindingPage.xaml,
        // as the sample code intentionally includes a binding failure.
        if (e.Message.Contains("NonExistentProperty"))
        {
            return;
        }

        throw new Exception($"A debug binding failed: {e.Message}");
    }

    private static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
    }

    private static void OnUnhandledException(object sender, System.UnhandledExceptionEventArgs e)
    {
        OnUnhandledException(e.ExceptionObject as Exception ?? new Exception("Unknown error"));
    }

    private static void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        e.Handled = true;

        OnUnhandledException(e.Exception);
    }

    private static void OnUnhandledException(Exception exception)
    {
        try
        {
            // Create the notification.
            var notification = new AppNotificationBuilder()
                .AddText("An exception was thrown.")
                .AddText($"Type: {exception.GetType()}")
                .AddText($"Message: {exception.Message}\r\nHResult: {exception.HResult}")
                .BuildNotification();

            // Show the notification
            AppNotificationManager.Default.Show(notification);
        }
        catch (Exception)
        {
            // Ignore.
        }
    }
}