using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services
{
    public class DialogService : IDialogService
    {
        public async Task<ImageData[]> SelectImages()
        {
            var mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : null;
            var fileDialog = new OpenFileDialog
            {
                Title = "Open image",
                AllowMultiple = true
            };
            var filenames = await fileDialog.ShowAsync(mainWindow);


            var images = new List<ImageData>();

            foreach (var filename in filenames)
            {
                var filebytes = await File.ReadAllBytesAsync(filename);
                var imageData = new ImageData(filename, filebytes);
            }

            return images.ToArray();
        }
    }
}