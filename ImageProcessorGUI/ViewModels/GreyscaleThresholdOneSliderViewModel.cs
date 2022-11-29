using ImageProcessorLibrary.DataStructures;
using ReactiveUI;
using System.Windows.Input;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.ViewModels;

public class GreyscaleThresholdOneSliderViewModel : ViewModelBase
{
    private int thresholdValue;

    public GreyscaleThresholdOneSliderViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }


    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }

    public int ThresholdValue
    {
        get => thresholdValue;
        set
        {
            thresholdValue = value;
            this.RaisePropertyChanged();
        }
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(() =>
    {
        var threshold = new ThresholdService();
        var newImageData = threshold.GreyscaleThresholdOneSlider(OriginalImageData, ThresholdValue);
        ImageData.Update(newImageData);
    });
}