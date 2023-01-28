using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.StretchingServices;

public class StretchingOptionsService : IStretchingOptionsService
{
    public ImageData GetEqualizedImage(ImageData imageData)
    {
        var stretchingService = new StretchingService();
        return stretchingService.EqualizeStretching(imageData);
    }
}