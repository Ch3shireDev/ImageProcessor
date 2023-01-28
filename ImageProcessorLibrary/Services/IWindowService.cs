using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IWindowService
{
    public void ShowImageWindow(ImageData imageData);
    public void ShowOptionsWindowOneValue(object viewModel);
    public void ShowOptionsWindowTwoValues(object viewModel);
    void ShowAddImagesViewModel(object addImagesViewModel);
    void ShowMathOperationViewModel(object mathOperationViewModel);
    void ShowBinaryOperationViewModel(object binaryOperationViewModel);
}