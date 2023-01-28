using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockStretchingOptionsWindowService : IStretchingOptionsService
{
    public bool IsLinearStretchingWindowShown { get; set; }
    public bool IsGammaStretchingWindowShown { get; set; }
    public bool IsEqualizeHistogramWindowShown { get; set; }

    public ImageData GetEqualizedImage(ImageData imageData)
    {
        return imageData;
    }

    public void ShowGammaStretchingWindow(ImageData imageData)
    {
        IsGammaStretchingWindowShown = true;
    }

    public void ShowLinearStretchingWindow(ImageData imageData)
    {
        IsLinearStretchingWindowShown = true;
    }
}