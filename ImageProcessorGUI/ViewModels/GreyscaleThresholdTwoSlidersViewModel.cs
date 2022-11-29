using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class GreyscaleThresholdTwoSlidersViewModel : ViewModelBase
{

    public GreyscaleThresholdTwoSlidersViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }
    
    private int _thresholdValue1;

    public int ThresholdValue1
    {
        get => _thresholdValue1;
        set
        {
            _thresholdValue1 = value;
            this.RaisePropertyChanged();
        }
    }

    private int _thresholdValue2;

    public int ThresholdValue2
    {
        get => _thresholdValue2;
        set
        {
            _thresholdValue2 = value;
            this.RaisePropertyChanged();
        }
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(() =>
    {
        var threshold = new ThresholdService();
        var newImageData = threshold.GreyscaleThresholdTwoSliders(OriginalImageData, ThresholdValue1, ThresholdValue2);
        ImageData.Update(newImageData);
    });
}