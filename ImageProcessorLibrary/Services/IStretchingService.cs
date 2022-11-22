namespace ImageProcessorLibrary.Services;

public interface IStretchingService
{
    void LinearStretching();

    void GammaStretching();

    void EqualizeHistogram();
}