using Windows.UI;

using Microsoft.UI.Xaml;

namespace Atelier.Helpers;

internal class TitleBarHelper
{
    public static void SetCaptionButtonColors(Window window, Color color)
    {
        var res = Application.Current.Resources;
        res["WindowCaptionForeground"] = color;
        window.AppWindow.TitleBar.ButtonForegroundColor = color;
    }

    public static void SetCaptionButtonBackgroundColors(Window window, Color? color)
    {
        var titleBar = window.AppWindow.TitleBar;
        titleBar.ButtonBackgroundColor = color;
    }

    public static void SetForegroundColor(Window window, Color? color)
    {
        var titleBar = window.AppWindow.TitleBar;
        titleBar.ForegroundColor = color;
    }

    public static void SetBackgroundColor(Window window, Color? color)
    {
        var titleBar = window.AppWindow.TitleBar;
        titleBar.BackgroundColor = color;
    }
}
