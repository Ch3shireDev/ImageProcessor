﻿using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockProcessService : IProcessService
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