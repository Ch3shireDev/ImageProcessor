using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class FileService : IFileService
{
    private readonly IDialogService _dialogService;
    private readonly IWindowService _windowService;

    public FileService(IDialogService dialogService, IWindowService windowService)
    {
        _dialogService = dialogService;
        _windowService = windowService;
    }

    public void SaveImage(ImageData imageData)
    {
    }

    public async Task OpenImage()
    {
        var images = await _dialogService.SelectImages();
        foreach (var image in images)
        {
            _windowService.ShowImageWindow(image);
        }
    }

    public void DuplicateImage(ImageData imageData)
    {
        _windowService.ShowImageWindow(imageData);
    }
}