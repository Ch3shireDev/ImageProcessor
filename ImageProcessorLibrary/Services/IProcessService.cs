namespace ImageProcessorLibrary.Services;

public interface IProcessService
{
    IImageData NegateImage(IImageData imageData);
}