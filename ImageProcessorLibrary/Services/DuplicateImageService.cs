using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class DuplicateImageService : IDuplicateImageService
{
    private readonly IWindowService _windowService;

    public DuplicateImageService(IWindowService windowService)

    {
        _windowService = windowService;
    }

    public void DuplicateImage(ImageData imageData)
    {
        var newImageData = new ImageData(imageData);
        _windowService.ShowImageWindow(newImageData);
    }
}