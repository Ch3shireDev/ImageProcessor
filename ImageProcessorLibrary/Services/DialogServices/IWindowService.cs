using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.DialogServices;

/// <summary>
///     Interfejs do obsługi okienek.
/// </summary>
public interface IWindowService
{
    /// <summary>
    ///     Wyświetla okienko z obrazem.
    /// </summary>
    /// <param name="imageData"></param>
    public void ShowImageWindow(ImageData imageData);

    /// <summary>
    ///     Wyświetla okno opcji na podstawie modelu widoku z jednym modyfikowalnym parametrem.
    /// </summary>
    /// <param name="viewModel"></param>
    public void ShowOptionsWindowOneValue(object viewModel);

    /// <summary>
    ///     Wyświetla okno opcji na podstawie modelu widoku z dwoma modyfikowalnymi parametrami.
    /// </summary>
    /// <param name="viewModel"></param>
    public void ShowOptionsWindowTwoValues(object viewModel);

    /// <summary>
    ///     Wyświetla okno opcji na podstawie modelu widoku z dodawaniem nowych obrazów.
    /// </summary>
    /// <param name="addImagesViewModel"></param>
    void ShowAddImagesViewModel(object addImagesViewModel);

    /// <summary>
    ///     Wyświetla okno opcji na podstawie modelu widoku z operacjami matematycznymi.
    /// </summary>
    /// <param name="mathOperationViewModel"></param>
    void ShowMathOperationViewModel(object mathOperationViewModel);

    /// <summary>
    ///     Wyświetla okno opcji na podstawie modelu widoku z operacjami binarnymi.
    /// </summary>
    /// <param name="binaryOperationViewModel"></param>
    void ShowBinaryOperationViewModel(object binaryOperationViewModel);
}