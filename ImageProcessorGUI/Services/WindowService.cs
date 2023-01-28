using ImageProcessorGUI.Models;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services;

public class WindowService : IWindowService
{
    public IImageServiceProvider ImageServiceProvider { get; set; }

    public void ShowImageWindow(ImageData imageData)
    {
        var imageModel = new MainModel(imageData, ImageServiceProvider);

        var viewModel = new MainWindowViewModel(imageModel);

        var mainWindow = new MainWindow
        {
            DataContext = viewModel
        };

        mainWindow.Show();
    }

    public void ShowOptionsWindowOneValue(object viewModel)
    {
        var optionsWindow = new OptionsOneValueWindow
        {
            DataContext = viewModel
        };

        optionsWindow.Show();
    }

    public void ShowOptionsWindowTwoValues(object viewModel)
    {
        var optionsWindow = new OptionsTwoValuesWindow
        {
            DataContext = viewModel
        };

        optionsWindow.Show();
    }

    public void ShowAddImagesViewModel(object addImagesViewModel)
    {
        var addImagesWindow = new AddImagesWindow
        {
            DataContext = addImagesViewModel
        };

        addImagesWindow.Show();
    }

    public void ShowMathOperationViewModel(object mathOperationViewModel)
    {
        var mathOperationWindow = new MathOperationWindow
        {
            DataContext = mathOperationViewModel
        };

        mathOperationWindow.Show();
    }

    public void ShowBinaryOperationViewModel(object binaryOperationViewModel)
    {
        var binaryOperationWindow = new BinaryOperationsWindow
        {
            DataContext = binaryOperationViewModel
        };

        binaryOperationWindow.Show();
    }
}