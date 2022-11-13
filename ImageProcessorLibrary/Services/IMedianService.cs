namespace ImageProcessorLibrary.Services;

public interface IMedianService
{
    void CalculateMedian3x3();

    void CalculateMedian5x5();

    void CalculateMedian7x7();

    void CalculateMedian9x9();
}