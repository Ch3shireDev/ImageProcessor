using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface ISaveImageDialogService
{
    Task<string?> GetSaveImageFileName(IImageData imageData);
}