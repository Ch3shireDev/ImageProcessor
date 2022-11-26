using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class GammaStretchingViewModel : ViewModelBase
{
    private readonly StretchingService stretchingService = new();

    private double gammaValue = 1;

    private readonly ImageData ImageData;
    private readonly ImageData OriginalImageData;

    public GammaStretchingViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public string GammaValueText { get; set; }

    public double GammaValue
    {
        get => gammaValue;
        set
        {
            gammaValue = value;
            GammaValueText = value.ToString();
            this.RaisePropertyChanged();
            this.RaisePropertyChanged(nameof(GammaValueText));
        }
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(() =>
    {
        var result = stretchingService.GammaStretching(OriginalImageData, GammaValue);
        ImageData.Update(result);
    });
}