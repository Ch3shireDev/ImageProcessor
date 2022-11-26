using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockWindowService : IWindowService
{
    public bool IsShowImageWindowCalled { get; set; }
    public bool IsOptionWindowCalled { get; set; }
    public ImageData ImageData { get; set; }

    public void ShowImageWindow(ImageData imageData)
    {
        IsShowImageWindowCalled = true;
        ImageData = imageData;
    }

    public void ShowLinearStretchingWindow(ImageData imageData)
    {
        IsOptionWindowCalled = true;
    }
}