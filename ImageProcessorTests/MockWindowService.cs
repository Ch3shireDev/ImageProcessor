using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

public class MockWindowService : IWindowService
{
    public bool IsShowImageWindowCalled { get; set; }
    public void ShowImageWindow(ImageData imageData)
    {
        IsShowImageWindowCalled = true;
    }
}