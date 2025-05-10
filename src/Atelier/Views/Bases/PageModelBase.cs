using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI;

using Microsoft.UI.Xaml.Controls;

namespace Atelier.Views.Bases;

/// <summary>
/// PageModelBase is a base class for pages that use a ViewModel.
/// It provides a way to access the ViewModel.
/// </summary>
/// <typeparam name="TViewModel">ViewModel</typeparam>
public abstract class PageModelBase<TViewModel> : Page
    where TViewModel : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageModelBase{TViewModel}"/> class.
    /// </summary>
    protected PageModelBase()
    {
        ViewModel = Ioc.Default.GetService<TViewModel>() ?? throw new ArgumentNullException(typeof(TViewModel).FullName);
    }

    /// <summary>
    /// ViewModel for the page.
    /// </summary>
    public TViewModel ViewModel { get; }

    /// <summary>
    /// Invoke the action on the UI thread.
    /// </summary>
    /// <param name="action">action</param>
    protected virtual void InvokeOnDispatcher(Action action)
    {
        if (DispatcherQueue.HasThreadAccess)
        {
            action.Invoke();
        }
        else
        {
            DispatcherQueue.TryEnqueue(action.Invoke);
        }
    }

    /// <summary>
    /// Invoke the action on the UI thread with task.
    /// </summary>
    /// <param name="action">action</param>
    /// <returns>Task</returns>
    protected virtual async Task InvokeOnDispatcherAsync(Action action)
    {
        if (DispatcherQueue.HasThreadAccess)
        {
            action.Invoke();
        }
        else
        {
            await DispatcherQueue.EnqueueAsync(action.Invoke).ConfigureAwait(false);
        }
    }
}