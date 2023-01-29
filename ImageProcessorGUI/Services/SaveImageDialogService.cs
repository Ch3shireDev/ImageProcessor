using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorGUI.Services;

/// <summary>
///     Serwis okna zapisu obrazu.
/// </summary>
public class SaveImageDialogService : ISaveImageDialogService
{
    private Window? MainWindow =>
        Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    /// <summary>
    ///     Zwraca wybraną przez użytkownika ścieżkę pliku do zapisu obrazu.
    /// </summary>
    /// <param name="imageData">Obiekt obrazu do zapisu.</param>
    /// <returns></returns>
    public async Task<string?> GetSaveImageFileName(ImageData imageData)
    {
        var fileDialog = new SaveFileDialog
        {
            Title = "Save image",
            //Directory = null,
            //Filters = null,
            InitialFileName = imageData.Filepath,
            DefaultExtension = imageData.Extension
        };

        var filename = await fileDialog.ShowAsync(MainWindow);

        return filename;
    }
}