using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IWindowService
{
    public void ShowImageWindow(ImageData imageData);
    public void ShowOptionWindow(ImageData imageData);
}