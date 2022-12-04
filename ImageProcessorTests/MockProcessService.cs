using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

public class MockProcessService:IProcessService
{
    public bool IsNegated{ get; set; }
    public IImageData NegateImage(IImageData imageData)
    {
        IsNegated = true;
        return imageData;
    }

    public void OpenBinaryThresholdWindow(IImageData imageData)
    {
        IsBinaryThresholdWindowOpen = true;
    }

    public bool IsBinaryThresholdWindowOpen { get; set; }

    public void OpenGreyscaleThresholdOneSliderWindow(IImageData imageData)
    {
        IsGreyscaleThresholdWindowOpen = true;
    }

    public void OpenGreyscaleThresholdTwoSlidersWindow(IImageData imageData)
    {
        IsGreyscaleThresholdTwoSlidersWindowOpen = true;
    }

    public bool IsGreyscaleThresholdTwoSlidersWindowOpen { get; set; }

    public bool IsGreyscaleThresholdWindowOpen { get; set; }
}