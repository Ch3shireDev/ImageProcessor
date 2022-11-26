using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorGUI.ViewModels;

public class BinaryThresholdGrayscaleViewModel : ViewModelBase
{
    public BinaryThresholdGrayscaleViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }


    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }
}

public class BinaryThresholdGrayscaleTwoSlidersViewModel : ViewModelBase
{
    public BinaryThresholdGrayscaleTwoSlidersViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }
}