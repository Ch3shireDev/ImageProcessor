using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IStretchingOptionsService
{
    ImageData GetEqualizedImage(ImageData imageData);
}