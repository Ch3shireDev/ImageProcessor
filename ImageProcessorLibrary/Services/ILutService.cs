namespace ImageProcessorLibrary.Services;

public interface ILutService
{
    int[] GetIntensityHistogram(byte[]? image);
    int[] GetRedHistogram(byte[]? image);
    int[] GetGreenHistogram(byte[]? image);
    int[] GetBlueHistogram(byte[]? image);
}