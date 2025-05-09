using Atelier.Pages;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Atelier.DataContexts;

/// <summary>
/// <see cref="MainShellPage"/> DataContext
/// </summary>
public partial class MainPageDataContext : ObservableObject
{
    private bool _loadingState;

    /// <summary>
    /// Loading state of the page
    /// </summary>
    public bool LoadingState
    {
        get => _loadingState;
        set => SetProperty(ref _loadingState, value);
    }
}