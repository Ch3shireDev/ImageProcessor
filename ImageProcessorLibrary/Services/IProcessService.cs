namespace ImageProcessorLibrary.Services;

public interface IProcessService
{
    void NegateImage();

    void BinaryThreshold();

    void GreyscaleThreshold();
    
}