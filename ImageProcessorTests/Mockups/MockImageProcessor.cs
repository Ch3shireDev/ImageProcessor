using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.ImageServices;

namespace ImageProcessorTests.Mockups;

public class MockImageProcessor : IImageProcessor
{
    public bool IsNegated { get; set; }

    public bool IsBinaryThresholdWindowOpen { get; set; }

    public bool IsGreyscaleThresholdTwoSlidersWindowOpen { get; set; }

    public bool IsGreyscaleThresholdWindowOpen { get; set; }

    public ImageData NegateImage(ImageData imageData)
    {
        IsNegated = true;
        return imageData;
    }

    public ImageData SwapHorizontal(ImageData imageData)
    {
        IsHorizontalSwap = true;
        return imageData;
    }

    public bool IsHorizontalSwap { get; set; }

    public ImageData ToGrayscale(ImageData imageData)
    {
        return imageData;
    }

    public void OpenBinaryThresholdWindow(ImageData imageData)
    {
        IsBinaryThresholdWindowOpen = true;
    }

    public void OpenGreyscaleThresholdOneSliderWindow(ImageData imageData)
    {
        IsGreyscaleThresholdWindowOpen = true;
    }

    public void OpenGreyscaleThresholdTwoSlidersWindow(ImageData imageData)
    {
        IsGreyscaleThresholdTwoSlidersWindowOpen = true;
    }
}