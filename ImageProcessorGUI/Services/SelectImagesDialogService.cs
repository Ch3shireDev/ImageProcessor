using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorGUI.Services;


/// <summary>
/// Serwis do obsługi okna dialogowego wyboru obrazu.
/// </summary>
public class SelectImagesDialogService : ISelectImagesDialogService
{
    private Window? MainWindow =>
        Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    public async Task<ImageData[]> SelectImages(bool allowMultiple = false)
    {
        var fileDialog = new OpenFileDialog
        {
            Title = "Open image",
            AllowMultiple = allowMultiple
        };
        var filenames = await fileDialog.ShowAsync(MainWindow);
        if (filenames == null) return Array.Empty<ImageData>();

        var images = new List<ImageData>();

        foreach (var filename in filenames)
        {
            var filebytes = await File.ReadAllBytesAsync(filename);
            var imageData = new ImageData(filename, filebytes);
            images.Add(imageData);
        }

        return images.ToArray();
    }
}