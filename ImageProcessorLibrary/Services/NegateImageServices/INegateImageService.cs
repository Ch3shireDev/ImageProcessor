using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.NegateImageServices;

public interface INegateImageService
{
    ImageData NegateImage(ImageData imageData);
}