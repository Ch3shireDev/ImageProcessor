using ImageProcessorGUI.Models;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services;

public class WindowService : IWindowService
{
    public void ShowImageWindow(ImageData imageData)
    {
        var imageModel = new ImageModel
        {
            ImageData = imageData
        };

        var menuViewModel = new MenuViewModel
        {
            ImageModel = imageModel
        };

        var viewModel = new MainWindowViewModel
        {
            MenuViewModel = menuViewModel
        };

        var mainWindow = new MainWindow
        {
            DataContext = viewModel
        };

        mainWindow.Show();
    }
}