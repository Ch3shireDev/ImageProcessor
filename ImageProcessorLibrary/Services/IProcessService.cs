using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IProcessService
{
    ImageData NegateImage(ImageData imageData);
}