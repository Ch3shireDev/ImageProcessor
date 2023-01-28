using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.DuplicateImageServices;

public interface IDuplicateImageService
{
    public void DuplicateImage(ImageData imageData);
}