using ImageProcessorGUI.Models;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorGUI.Services;

/// <summary>
///     Serwis okien.
/// </summary>
public class WindowService : IWindowService
{
    public IImageServiceProvider imageServiceProvider { get; set; }

    public void ShowImageWindow(ImageData imageData)
    {
        var imageModel = new MainModel(imageData, imageServiceProvider);

        var viewModel = new MainWindowViewModel(imageModel);

        var mainWindow = new MainWindow
        {
            DataContext = viewModel
        };

        mainWindow.Show();
    }

    /// <summary>
    ///     Wyświetla okno z jedną opcją.
    /// </summary>
    /// <param name="viewModel"></param>
    public void ShowOptionsWindowOneValue(object viewModel)
    {
        var optionsWindow = new OptionsOneValueWindow
        {
            DataContext = viewModel
        };

        optionsWindow.Show();
    }

    /// <summary>
    ///     Wyświetla okno z dwiema opcjami.
    /// </summary>
    /// <param name="viewModel"></param>
    public void ShowOptionsWindowTwoValues(object viewModel)
    {
        var optionsWindow = new OptionsTwoValuesWindow
        {
            DataContext = viewModel
        };

        optionsWindow.Show();
    }

    /// <summary>
    ///     Wyświetla okno dodawania obrazów.
    /// </summary>
    /// <param name="addImagesViewModel"></param>
    public void ShowAddImagesViewModel(object addImagesViewModel)
    {
        var addImagesWindow = new AddImagesWindow
        {
            DataContext = addImagesViewModel
        };

        addImagesWindow.Show();
    }

    /// <summary>
    ///    Wyświetla okno z operacją matematyczną.
    /// </summary>
    /// <param name="mathOperationViewModel"></param>
    public void ShowMathOperationViewModel(object mathOperationViewModel)
    {
        var mathOperationWindow = new MathOperationWindow
        {
            DataContext = mathOperationViewModel
        };

        mathOperationWindow.Show();
    }

    /// <summary>
    ///    Wyświetla okno z operacją binarną.
    /// </summary>
    /// <param name="binaryOperationViewModel"></param>
    public void ShowBinaryOperationViewModel(object binaryOperationViewModel)
    {
        var binaryOperationWindow = new BinaryOperationsWindow
        {
            DataContext = binaryOperationViewModel
        };

        binaryOperationWindow.Show();
    }
}