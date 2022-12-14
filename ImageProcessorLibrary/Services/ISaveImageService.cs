using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface ISaveImageService
{
    public Task SaveImageAsync(IImageData imageData);
}