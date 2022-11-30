using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels.Old;

internal class EqualizeStretchingViewModel : ViewModelBase
{

    public EqualizeStretchingViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    ImageData ImageData;
    ImageData OriginalImageData;
    private int _lowerLimit;
    private int _upperLimit;

    private StretchingService stretchingService = new StretchingService();

    public ICommand RefreshCommand => ReactiveCommand.Create(() =>
    {
        var result = stretchingService.EqualizeStretching(OriginalImageData);
        ImageData.Update(result);

    });

    public int LowerLimit
    {
        get => _lowerLimit;
        set
        {
            _lowerLimit = value;
            this.RaisePropertyChanged();
        }
    }

    public int UpperLimit
    {
        get => _upperLimit;
        set
        {
            _upperLimit = value;
            this.RaisePropertyChanged();
        }
    }
}