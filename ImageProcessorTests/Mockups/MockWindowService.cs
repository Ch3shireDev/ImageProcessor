using ImageProcessorGUI.Models;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockWindowService : IWindowService
{
    public bool IsShowImageWindowCalled { get; set; }
    public ImageData ImageData { get; set; }

    public void ShowImageWindow(ImageData imageData)
    {
        IsShowImageWindowCalled = true;
        ImageData = imageData;
    }
}