namespace ImageProcessorLibrary.Services;

public interface IBlurService
{
    void GaussianBlur();
    void MedianBlur();
    void Sharpening();
}