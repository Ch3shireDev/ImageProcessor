using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services;

public class StretchingOptionsService : IStretchingOptionsService
{

    public ImageData GetEqualizedImage(ImageData imageData)
    {
        var stretchingService = new StretchingService();
        return stretchingService.EqualizeStretching(imageData);
    }
}