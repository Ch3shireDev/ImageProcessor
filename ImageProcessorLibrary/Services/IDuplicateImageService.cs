using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IDuplicateImageService
{
    public void DuplicateImage(ImageData imageData);
}