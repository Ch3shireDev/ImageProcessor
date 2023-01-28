using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace ImageProcessorGUI;

internal class Program
{
    /// <summary>
    /// Kod inicjalizacyjny.
    /// </summary>
    /// <param name="args"></param>
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    /// <summary>
    /// Konfiguracja Avalonii.
    /// </summary>
    /// <returns></returns>
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }
}