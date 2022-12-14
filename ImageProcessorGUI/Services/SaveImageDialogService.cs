using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services;

public class SaveImageDialogService : ISaveImageDialogService
{
    private Window? MainWindow =>
        Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    public async Task<string?> GetSaveImageFileName(IImageData imageData)
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