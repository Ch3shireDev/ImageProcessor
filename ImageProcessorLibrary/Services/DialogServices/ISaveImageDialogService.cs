using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.DialogServices;

public interface ISaveImageDialogService
{
    Task<string?> GetSaveImageFileName(ImageData imageData);
}