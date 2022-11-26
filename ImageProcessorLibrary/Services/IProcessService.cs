using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IProcessService
{
    ImageData NegateImage(ImageData imageData);

    void BinaryThreshold(ImageData imageData);
    
    void GreyscaleThreshold(ImageData imageData);
    void GreyscaleThresholdTwoSliders(ImageData imageData);
}

public class ProcessService:IProcessService
{
    public ImageData NegateImage(ImageData imageData)
    {
        return imageData;
    }

    public void BinaryThreshold(ImageData imageData)
    {
        
    }

    public void GreyscaleThreshold(ImageData imageData)
    {
        throw new NotImplementedException();
    }

    public void GreyscaleThresholdTwoSliders(ImageData imageData)
    {
        throw new NotImplementedException();
    }
}