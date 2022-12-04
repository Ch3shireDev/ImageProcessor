using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IHistogramService
{
    IImageData GetValueHistogram(IImageData imageData);
    IImageData GetRgbHistogram(IImageData imageData);
    IImageData GetRedHistogram(IImageData imageData);
    IImageData GetGreenHistogram(IImageData imageData);
    IImageData GetBlueHistogram(IImageData imageData);
}