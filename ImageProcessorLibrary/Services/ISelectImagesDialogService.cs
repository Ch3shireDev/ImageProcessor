using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface ISelectImagesDialogService
{
    Task<ImageData[]> SelectImages();
}

public interface ISaveImageDialogService
{
    Task<string?>  GetSaveImageFileName(ImageData imageData);
}