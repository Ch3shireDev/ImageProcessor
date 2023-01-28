using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.ImageServices;

public interface ISaveImageService
{
    public Task SaveImageAsync(ImageData imageData);
}