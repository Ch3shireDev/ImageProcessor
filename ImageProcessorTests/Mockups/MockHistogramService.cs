using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockHistogramService : IHistogramService
{
    public bool IsShowValueHistogramCalled { get; set; }
    public bool IsShowRgbHistogramCalled { get; set; }
    public bool IsShowRHistogramCalled { get; set; }
    public bool IsShowGHistogramCalled { get; set; }
    public bool IsShowBHistogramCalled { get; set; }

    public ImageData GetValueHistogram(ImageData imageData)
    {
        IsShowValueHistogramCalled = true;
        return imageData;
    }

    public ImageData GetRgbHistogram(ImageData imageData)
    {
        IsShowRgbHistogramCalled = true;
        return imageData;
    }

    public ImageData GetRedHistogram(ImageData imageData)
    {
        IsShowRHistogramCalled = true;
        return imageData;
    }

    public ImageData GetGreenHistogram(ImageData imageData)
    {
        IsShowGHistogramCalled = true;
        return imageData;
    }

    public ImageData GetBlueHistogram(ImageData imageData)
    {
        IsShowBHistogramCalled = true;
        return imageData;
    }
}