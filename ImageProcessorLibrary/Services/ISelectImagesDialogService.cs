using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface ISelectImagesDialogService
{
    Task<ImageData[]> SelectImages(bool selectMultiple=false);
}