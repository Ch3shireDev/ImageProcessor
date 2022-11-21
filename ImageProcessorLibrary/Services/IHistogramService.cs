using ImageProcessorLibrary.DataStructures;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace ImageProcessorLibrary.Services;

public interface IHistogramService
{
    ImageData GetValueHistogram(ImageData imageData);
    ImageData GetRgbHistogram(ImageData imageData);
    ImageData GetRedHistogram(ImageData imageData);
    ImageData GetGreenHistogram(ImageData imageData);
    ImageData GetBlueHistogram(ImageData imageData);
}