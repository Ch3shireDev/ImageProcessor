using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels.Old;

public class BinaryThresholdViewModel : ViewModelBase
{
    private int thresholdValue;

    public BinaryThresholdViewModel(ImageData imageData)
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
            //Refresh();
        }
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(() => { Refresh(); });

    private void Refresh()
    {
        var threshold = new ThresholdService();
        var newImageData = threshold.BinaryThreshold(OriginalImageData, ThresholdValue);
        ImageData.Update(newImageData);
    }
}