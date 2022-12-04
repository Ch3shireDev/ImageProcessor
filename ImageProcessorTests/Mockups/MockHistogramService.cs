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

    public IImageData GetValueHistogram(IImageData imageData)
    {
        IsShowValueHistogramCalled = true;
        return imageData;
    }

    public IImageData GetRgbHistogram(IImageData imageData)
    {
        IsShowRgbHistogramCalled = true;
        return imageData;
    }

    public IImageData GetRedHistogram(IImageData imageData)
    {
        IsShowRHistogramCalled = true;
        return imageData;
    }

    public IImageData GetGreenHistogram(IImageData imageData)
    {
        IsShowGHistogramCalled = true;
        return imageData;
    }

    public IImageData GetBlueHistogram(IImageData imageData)
    {
        IsShowBHistogramCalled = true;
        return imageData;
    }
}