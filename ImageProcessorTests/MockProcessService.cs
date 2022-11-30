using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

public class MockProcessService:IProcessService
{
    public bool IsNegated{ get; set; }
    public ImageData NegateImage(ImageData imageData)
    {
        IsNegated = true;
        return imageData;
    }

    public void OpenBinaryThresholdWindow(ImageData imageData)
    {
        IsBinaryThresholdWindowOpen = true;
    }

    public bool IsBinaryThresholdWindowOpen { get; set; }

    public void OpenGreyscaleThresholdOneSliderWindow(ImageData imageData)
    {
        IsGreyscaleThresholdWindowOpen = true;
    }

    public void OpenGreyscaleThresholdTwoSlidersWindow(ImageData imageData)
    {
        IsGreyscaleThresholdTwoSlidersWindowOpen = true;
    }

    public bool IsGreyscaleThresholdTwoSlidersWindowOpen { get; set; }

    public bool IsGreyscaleThresholdWindowOpen { get; set; }
}