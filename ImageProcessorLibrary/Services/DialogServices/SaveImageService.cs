using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.ImageServices;

namespace ImageProcessorLibrary.Services.DialogServices;

public class SaveImageService : ISaveImageService
{
    private readonly IFileSystemService _fileSystemService;
    private readonly ISaveImageDialogService _saveImageDialogService;

    public SaveImageService(ISaveImageDialogService saveImageDialogService, IFileSystemService fileSystemService)
    {
        _saveImageDialogService = saveImageDialogService;
        _fileSystemService = fileSystemService;
    }

    public async Task SaveImageAsync(ImageData imageData)
    {
        var filename = await _saveImageDialogService.GetSaveImageFileName(imageData);
        if (filename != null) await _fileSystemService.WriteAllBytesAsync(filename, imageData.Filebytes);
    }
}