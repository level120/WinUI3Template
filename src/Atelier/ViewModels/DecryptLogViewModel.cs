using System.Collections.ObjectModel;

using Windows.UI.Shell;

using Atelier.Models;
using Atelier.ViewModels.Bases;

namespace Atelier.ViewModels;

/// <summary>
/// DecryptLogViewModel is the ViewModel for the DecryptLog page.
/// </summary>
public sealed partial class DecryptLogViewModel : ViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecryptLogViewModel"/> class.
    /// </summary>
    public DecryptLogViewModel()
    {
        Task.Run(LoadLogItemAsync);
    }

    /// <summary>
    /// LogItems is the collection of log items displayed in the DecryptLog page.
    /// </summary>
    public ObservableCollection<LogItem> LogItems { get; } = [];

    private async Task LoadLogItemAsync()
    {
        await foreach (var item in GetLogItemsAsync().ConfigureAwait(false))
        {
            _ = InvokeOnDispatcherAsync(() => LogItems.Add(item));
        }
    }

    private static async IAsyncEnumerable<LogItem> GetLogItemsAsync()
    {
        foreach (var index in Enumerable.Range(0, 100000))
        {
            await Task.Delay(10).ConfigureAwait(false);

            yield return new LogItem($"[{index:D5}] {Guid.NewGuid()}");
        }
    }
}