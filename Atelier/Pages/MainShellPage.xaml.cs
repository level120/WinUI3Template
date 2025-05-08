using Atelier.DataContexts;

using CommunityToolkit.WinUI;

namespace Atelier.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainShellPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainShellPage"/> class.
    /// </summary>
    public MainShellPage()
    {
        InitializeComponent();

        DataContext = ModelContext = new MainPageDataContext();

        ModelContext.LoadingState = true;

        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

            await DispatcherQueue.EnqueueAsync(() =>
            {
                ModelContext.LoadingState = false;
            });
        });
    }

    /// <summary>
    /// Model context for the page.
    /// </summary>
    public MainPageDataContext ModelContext { get; }
}
