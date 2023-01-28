using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.ImageServices;

public interface IDuplicateImageService
{
    public void DuplicateImage(ImageData imageData);
}