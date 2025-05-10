using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Atelier.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Shell
{
    private readonly SlideNavigationTransitionInfo _slideTransition = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Shell"/> class.
    /// </summary>
    public Shell()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        NavigatePage(typeof(HomePage));
    }

    private void OnPaneToggleRequested(TitleBar sender, object args)
    {
        NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
    }

    private void OnNavItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            NavigatePage(typeof(SettingsPage));
        }
        else if (args.InvokedItemContainer is NavigationViewItem { Tag: string pageName })
        {
            var pageType = pageName switch
            {
                "DefaultHome" => typeof(HomePage),
                _ => typeof(HomePage),
            };

            NavigatePage(pageType);
        }
        else
        {
            NavigatePage(typeof(HomePage));
        }
    }

    private void NavigatePage(Type type)
    {
        if (ContentFrame.Content?.GetType() != type)
        {
            ContentFrame.Navigate(type, null, _slideTransition);
        }
    }
}
