using Atelier.ViewModels.Bases;
using Atelier.Views;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Atelier.ViewModels;

/// <summary>
/// <see cref="Shell"/> DataContext
/// </summary>
public partial class ShellViewModel : ViewModelBase
{
    /// <summary>
    /// Loading State
    /// </summary>
    [ObservableProperty]
    public partial bool LoadingState { get; set; }
}