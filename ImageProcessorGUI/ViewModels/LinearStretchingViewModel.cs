using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class LinearStretchingViewModel : ViewModelBase
{
    private int _lMin = 0;
    private int _lMax = 255;

    public StretchingService stretchingService = new StretchingService();

    public LinearStretchingViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public ImageData OriginalImageData { get; set; }

    private ImageData ImageData { get; }
    public string Text1 { get; set; } = "0";
    public string Text2 { get; set; } = "255";

    public ICommand RefreshCommand => ReactiveCommand.Create(() =>
    {
        var result = stretchingService.LinearStretching(OriginalImageData, LMin, LMax);
        ImageData.Update(result);
    });

    public int LMin
    {
        get => _lMin;
        set
        {
            if (value < 0) value = 0;
            if (value > LMax) value = LMax;
            _lMin = value;
            Text1 = value.ToString();
            this.RaisePropertyChanged();
            this.RaisePropertyChanged(nameof(Text1));
        }
    }

    public int LMax
    {
        get => _lMax;
        set
        {
            if (value < LMin) value = LMin;
            _lMax = value;
            Text2 = value.ToString();
            this.RaisePropertyChanged();
            this.RaisePropertyChanged(nameof(Text2));
        }
    }
}