using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IProcessService
{
    ImageData NegateImage(ImageData imageData);

    void OpenBinaryThresholdWindow(ImageData imageData);

    void OpenGreyscaleThresholdOneSliderWindow(ImageData imageData);
    void OpenGreyscaleThresholdTwoSlidersWindow(ImageData imageData);
}