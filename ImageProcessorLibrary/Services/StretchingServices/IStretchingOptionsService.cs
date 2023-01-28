using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.StretchingServices;

public interface IStretchingOptionsService
{
    ImageData GetEqualizedImage(ImageData imageData);
}