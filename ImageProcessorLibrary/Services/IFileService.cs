using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IFileService
{
    public void SaveImage(ImageData imageData);
    public Task OpenImage();
    public void DuplicateImage(ImageData imageData);
}