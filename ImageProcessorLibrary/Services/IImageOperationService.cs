namespace ImageProcessorLibrary.Services;

public interface IImageOperationService
{
    void AddImage();

    void AddImageWithoutSaturate();

    void ImagesDifference();

    void AddNumberToImage();

    void SubtractNumberFromImage();
}