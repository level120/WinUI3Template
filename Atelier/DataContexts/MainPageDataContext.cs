using Atelier.Pages;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Atelier.DataContexts;

/// <summary>
/// <see cref="MainShellPage"/> DataContext
/// </summary>
public partial class MainPageDataContext : ObservableObject
{
    /// <summary>
    /// Loading state of the page
    /// </summary>
    [ObservableProperty]
    private bool _loadingState;
}