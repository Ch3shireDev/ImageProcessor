namespace ImageProcessorLibrary.Services;

public interface IHistogramService
{
    void ShowValueHistogram();

    void ShowRgbHistogram();

    void ShowRHistogram();

    void ShowGHistogram();

    void ShowBHistogram();
}