using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.SaveImageServices;

public interface ISaveImageService
{
    public Task SaveImageAsync(ImageData imageData);
}