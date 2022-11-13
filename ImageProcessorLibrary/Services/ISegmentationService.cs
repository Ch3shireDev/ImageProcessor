namespace ImageProcessorLibrary.Services;

public interface ISegmentationService
{
    
    void OtsuSegmentation();

    void AdaptativeThresholdSegmentation();

}