using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI;

using Microsoft.UI.Dispatching;

namespace Atelier.ViewModels.Bases;

/// <summary>
/// ViewModelBase is a base class for ViewModels that use the CommunityToolkit.Mvvm library.
/// </summary>
public abstract class ViewModelBase : ObservableObject
{
    /// <summary>
    /// Current DispatcherQueue.
    /// </summary>
    protected static DispatcherQueue CurrentDispatcherQueue => DispatcherQueue.GetForCurrentThread();

    /// <summary>
    /// Invoke the action on the UI thread.
    /// </summary>
    /// <param name="action">action</param>
    protected virtual void InvokeOnDispatcher(Action action)
    {
        var dispatcherQueue = CurrentDispatcherQueue;

        if (dispatcherQueue.HasThreadAccess)
        {
            action.Invoke();
        }
        else
        {
            dispatcherQueue.TryEnqueue(action.Invoke);
        }
    }

    /// <summary>
    /// Invoke the action on the UI thread with task.
    /// </summary>
    /// <param name="action">action</param>
    /// <returns>Task</returns>
    protected virtual async Task InvokeOnDispatcherAsync(Action action)
    {
        var dispatcherQueue = CurrentDispatcherQueue;

        if (dispatcherQueue.HasThreadAccess)
        {
            action.Invoke();
        }
        else
        {
            await dispatcherQueue.EnqueueAsync(action.Invoke).ConfigureAwait(false);
        }
    }
}