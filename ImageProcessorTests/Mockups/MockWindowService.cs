using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorTests.Mockups;

public class MockWindowService : IWindowService
{
    public bool IsShowImageWindowCalled { get; set; }
    public bool IsOptionWindowCalled { get; set; }
    public bool IsAddImagesWindowCalled { get; set; }
    public ImageData? ImageData { get; set; }

    public bool IsBinaryOperationsWindowCalled { get; set; }

    public bool IsMathOperationWindowCalled { get; set; }

    public object BinaryOperationViewModel { get; set; }

    public void ShowImageWindow(ImageData imageData)
    {
        IsShowImageWindowCalled = true;
        ImageData = imageData;
    }

    public void ShowOptionsWindowOneValue(object viewModel)
    {
        IsOptionWindowCalled = true;
    }

    public void ShowOptionsWindowTwoValues(object viewModel)
    {
        IsOptionWindowCalled = true;
    }

    public void ShowAddImagesViewModel(object addImagesViewModel)
    {
        IsAddImagesWindowCalled = true;
    }

    public void ShowMathOperationViewModel(object mathOperationViewModel)
    {
        IsMathOperationWindowCalled = true;
    }

    public void ShowBinaryOperationViewModel(object binaryOperationViewModel)
    {
        IsBinaryOperationsWindowCalled = true;
        BinaryOperationViewModel = binaryOperationViewModel;
    }
}