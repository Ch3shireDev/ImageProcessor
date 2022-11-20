using ImageProcessorGUI.Models;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services;

public class WindowService : IWindowService
{
    public IServiceProvider ServiceProvider { get; set; }
    
    public void ShowImageWindow(ImageData imageData)
    {
        var imageModel = new ImageModel(imageData, ServiceProvider);

        var viewModel = new MainWindowViewModel(imageModel);

        var mainWindow = new MainWindow
        {
            DataContext = viewModel
        };

        mainWindow.Show();
    }
}