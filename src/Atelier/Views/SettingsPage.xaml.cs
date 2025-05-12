// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Reflection;

using Microsoft.UI.Xaml;

using Windows.ApplicationModel.DataTransfer;

using Atelier.Helpers;

using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;

namespace Atelier.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPage"/> class.
    /// </summary>
    public SettingsPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Application version.
    /// </summary>
    public string Version { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";

    private void OnSettingsPageLoaded(object sender, RoutedEventArgs e)
    {
        ThemeMode.SelectedIndex = ThemeHelper.RootTheme switch
        {
            ElementTheme.Light => 0,
            ElementTheme.Dark => 1,
            ElementTheme.Default => 2,
            _ => ThemeMode.SelectedIndex
        };
    }

    private void OnCloneRepoCardClick(object sender, RoutedEventArgs e)
    {
        var package = new DataPackage();
        package.SetText(GitCloneTextBlock.Text);
        Clipboard.SetContent(package);
    }

    private void OnThemeModeChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedTheme = (ThemeMode.SelectedItem as ComboBoxItem)?.Tag?.ToString();
        var window = WindowHelper.GetWindowForElement(this);

        if (window is not null && selectedTheme is not null)
        {
            var theme = EnumHelper.GetEnum<ElementTheme>(selectedTheme);

            if (ThemeHelper.RootTheme != theme)
            {
                ThemeHelper.RootTheme = theme;

                TitleBarHelper.SetCaptionButtonColors(
                    window,
                    ThemeHelper.IsDarkTheme() ? Colors.White : Colors.Black);
            }
        }
    }
}
