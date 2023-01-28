using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.NegateImageServices;

public interface IImageProcessor
{
    ImageData NegateImage(ImageData imageData);
    ImageData SwapHorizontal(ImageData imageData);
    ImageData ToGrayscale(ImageData imageData);
}