using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.DialogServices;

public interface ISelectImagesDialogService
{
    Task<ImageData[]> SelectImages(bool selectMultiple = false);
}