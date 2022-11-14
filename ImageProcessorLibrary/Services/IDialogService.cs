using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IDialogService
{
    Task<ImageData[]> SelectImages();
}