using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.HistogramServices;

public interface IHistogramService
{
    ImageData GetValueHistogram(ImageData imageData);
    ImageData GetRgbHistogram(ImageData imageData);
    ImageData GetRedHistogram(ImageData imageData);
    ImageData GetGreenHistogram(ImageData imageData);
    ImageData GetBlueHistogram(ImageData imageData);
}