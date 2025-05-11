using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

using Microsoft.UI.Windowing;

using WinRT.Interop;

namespace Atelier.Helpers;

public class WindowHelper
{
    public static List<Window> ActiveWindows { get; } = [];

    public static Window CreateWindow()
    {
        var newWindow = new Window
        {
            SystemBackdrop = new MicaBackdrop()
        };

        TrackWindow(newWindow);

        return newWindow;
    }

    public static void TrackWindow(Window window)
    {
        window.Closed += (_, _) => ActiveWindows.Remove(window);

        ActiveWindows.Add(window);
    }

    public static AppWindow GetAppWindow(Window window)
    {
        var hWnd = WindowNative.GetWindowHandle(window);
        var wndId = Win32Interop.GetWindowIdFromWindow(hWnd);

        return AppWindow.GetFromWindowId(wndId);
    }

    public static Window? GetWindowForElement(UIElement element)
    {
        return element.XamlRoot != null
            ? ActiveWindows.FirstOrDefault(window => element.XamlRoot == window.Content.XamlRoot)
            : null;
    }

    public static double GetRasterizationScaleForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            if (ActiveWindows.Any(window => element.XamlRoot == window.Content.XamlRoot))
            {
                return element.XamlRoot.RasterizationScale;
            }
        }

        return 0.0;
    }
}