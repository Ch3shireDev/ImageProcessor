using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorLibrary.Services.DuplicateImageServices;

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