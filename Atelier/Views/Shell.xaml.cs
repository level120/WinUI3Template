using Microsoft.UI.Xaml.Controls;

namespace Atelier.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Shell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Shell"/> class.
    /// </summary>
    public Shell()
    {
        InitializeComponent();
    }

    private void OnPaneToggleRequested(TitleBar sender, object args)
    {
        NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
    }
}
