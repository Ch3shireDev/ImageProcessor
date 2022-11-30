using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IStretchingOptionsService
{
    void ShowGammaStretchingWindow(ImageData imageData);
    ImageData GetEqualizedImage(ImageData imageData);
    void ShowLinearStretchingWindow(ImageData imageData);
}